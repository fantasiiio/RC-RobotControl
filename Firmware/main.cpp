#include <string.h>

#include "mbed.h"
#include "Commands.h"
#include "flash.h"
#include "PPM.h"
#include "Servo.h"
#include "PCA9685.h"
#include "ChannelMapping.h"
#include "utils.h"
#include "fillBuffer.h"

/*
pinout:

SCL/SDA     I2C for servo output
D10         PPM input

*/

#define PPM_INPUT_PIN D10

#define COMMAND_MAX_SIZE 100
#define STREAM_BUFFER_SIZE 5

char command[COMMAND_MAX_SIZE];
int cmdIndex;
int commandReady;
int current_activity;
//bool updateServo = true;

PCA9685 pwm(D14,D15);
CommandList commands;
Serial pc(USBTX, USBRX);
void(*streamCallback)(char c);
PPM *ppm;
Timer timer10;
Servo servos[MAX_OUTPUT_CHANNELS] = {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15}; // Each servo knows his index
//float servoAngle[MAX_OUTPUT_CHANNELS] = {0};
//int robotMode = 3;
bool enableMapping = true;
int changeModeChIndex = -1;
float *inputValues;
bool enablePPM = true;

class ModeChMap
{
public: 
    ModeChMap() { min = -1.0; max = 1.0; }
    float min;
    float max;
    vector<ChannelMapping*> chMapList;  
};

#define MAX_MODE 3
ModeChMap modeChMapList[MAX_MODE];
vector<ChannelMapping*> *currentChMapList = NULL;

FillBuffer fillBuffer;

void setServoPulse(uint8_t n, float pulse) {
    pwm.setPWM(n, 0, pulse);
}

int VersionFormat = 0xAA04;
void saveChannelMappingList()
{
    int modeCount = MAX_MODE;
    fillBuffer.InitFillBuffer(FLASH_CHANNEL_MAPPING_ADDR);
    fillBuffer.AddFillBuffer(&VersionFormat, sizeof(int));
    fillBuffer.AddFillBuffer(&changeModeChIndex, sizeof(int));
    fillBuffer.AddFillBuffer(&modeCount, sizeof(int));
    for(int modeIndex = 0; modeIndex < modeCount; modeIndex++)
    {
        fillBuffer.AddFillBuffer(&modeChMapList[modeIndex].min, sizeof(float));
        fillBuffer.AddFillBuffer(&modeChMapList[modeIndex].max, sizeof(float));
        int size = modeChMapList[modeIndex].chMapList.size();       
        fillBuffer.AddFillBuffer(&size, sizeof(int));
        for(int chMapIndex = 0; chMapIndex < size; chMapIndex++) 
        {
            ChannelMapping *chMap = modeChMapList[modeIndex].chMapList[chMapIndex];
            chMap->Save(fillBuffer);
        }
    }
    fillBuffer.WriteFillBuffer();
}

void loadChannelMappingList(ModeChMap* modeChMapList)
{
    fillBuffer.InitFillBuffer(FLASH_CHANNEL_MAPPING_ADDR);
    int format = (*(int*)fillBuffer.ReadFillBuffer(sizeof(int)));   
    if(format != VersionFormat) return; 
    changeModeChIndex = (*(int*)fillBuffer.ReadFillBuffer(sizeof(int)));    
    int modeCount = (*(int*)fillBuffer.ReadFillBuffer(sizeof(int)));    
    for(int modeIndex = 0; modeIndex < modeCount; modeIndex++)
    {
        modeChMapList[modeIndex].min = (*(float*)fillBuffer.ReadFillBuffer(sizeof(float)));
        modeChMapList[modeIndex].max = (*(float*)fillBuffer.ReadFillBuffer(sizeof(float)));
        int size = (*(int*)fillBuffer.ReadFillBuffer(sizeof(int)));
        for(int chMapIndex = 0; chMapIndex < size; chMapIndex++) 
        {
            ChannelMapping *chMap = new ChannelMapping(inputValues, servos);
            chMap->Load(fillBuffer);
            modeChMapList[modeIndex].chMapList.push_back(chMap);
        }
    }
}

void dumpAllModeChMap()
{
    int modeCount = MAX_MODE;
    printf("Change Mode Channel:%d\r\n",changeModeChIndex);
    for(int modeIndex = 0; modeIndex < modeCount; modeIndex++)
    {
        printf("Mode %d\r\n",modeIndex);//
        printf("  input min:%g\r\n",modeChMapList[modeIndex].min);
        printf("  input max:%g\r\n",modeChMapList[modeIndex].max);
        int size = modeChMapList[modeIndex].chMapList.size();       
        for(int chMapIndex = 0; chMapIndex < size; chMapIndex++) 
        {
            printf("  Channel Map %d:\r\n",chMapIndex);
            ChannelMapping *chMap = modeChMapList[modeIndex].chMapList[chMapIndex];
            chMap->dump();
        }
    }
}

void dumpModeChMapData(int modeIndex)
{
    int size = modeChMapList[modeIndex].chMapList.size();       
    for(int chMapIndex = 0; chMapIndex < size; chMapIndex++) 
    {
        ChannelMapping *chMap = modeChMapList[modeIndex].chMapList[chMapIndex];
		chMap->dumpData(modeIndex, chMapIndex);
    }   
}

void initServoDriver() {
    int i;
    pwm.begin();
    pwm.setPWMFreq(60); // set 60hz for generic servos
    pwm.frequencyI2C(100000); //400kHz fast I2C comunication
    for(i = 0; i < MAX_OUTPUT_CHANNELS;i++)
        servos[i].setController(&pwm);
}

// This function is called when a character goes into the RX buffer.
void rxCallback() {
    char c;
    c = pc.getc();
    
	if(c != '\n')
	{
		command[cmdIndex++] = c;
		command[cmdIndex] = 0;
	}
	
	if(cmdIndex == COMMAND_MAX_SIZE ||  c == '\r')
	{
		command[cmdIndex-1] = 0;
		commandReady = 1;
		cmdIndex = 0;
	}

}

bool checkParameterCount(int paramCount, int count, const char *commandName)
{
    if(paramCount != count)
    {
        printf("command %s: wrong parameter counts\r\n", commandName);
        return false;
    }   
    return true;
}

void parseServo(CommandArgs cmdArgs)
{
    int servoIndex, degree, reverse;
    float range, subtrim;
    char *servoCommand;

    servoCommand = cmdArgs.params[1];
    if(strcmp(servoCommand, "angle") == 0)
    {
        if(!checkParameterCount(cmdArgs.paramCount, 4, "servo"))
            return;
        servoIndex = atoi(cmdArgs.params[2]);
        float angle = atoi(cmdArgs.params[3]);
        servos[servoIndex].setPosition(angle);
        printf("servoState index:%d pwm:%g\r\n", servoIndex, servos[servoIndex].getPwmUs());		
    }
    else if(strcmp(servoCommand, "calibrate") == 0)
    {
        if(!checkParameterCount(cmdArgs.paramCount, 5, "servo"))
            return;
        servoIndex = atoi(cmdArgs.params[2]);
        range = (float)atoi(cmdArgs.params[3]) / 1000000.0;
        degree = atoi(cmdArgs.params[4]);
        servos[servoIndex].calibrate(range, degree);
        mcfg.servo_calibrate_degree[servoIndex] = degree;
        mcfg.servo_calibrate_range[servoIndex] = range;
        writeConfigToFlash();
    }
    else if(strcmp(servoCommand, "pwm") == 0)
    {
        if(!checkParameterCount(cmdArgs.paramCount, 4, "servo"))
            return;
        servoIndex = atoi(cmdArgs.params[2]);
        float pwm = atoi(cmdArgs.params[3]);
        servos[servoIndex].pwm(pwm);
        printf("servoState index:%d angle:%g\r\n", servoIndex, servos[servoIndex].getPosition());
		
    }
    else if(strcmp(servoCommand, "subtrim") == 0)
    {
        if(!checkParameterCount(cmdArgs.paramCount, 4, "servo"))
            return;
        servoIndex = atoi(cmdArgs.params[2]);
        subtrim = (float)atoi(cmdArgs.params[3]) / 1000000.0;
        servos[servoIndex].subTrim(subtrim);
        mcfg.servo_center[servoIndex] = servos[servoIndex].getCenter();
        writeConfigToFlash();
        printf("servoState index:%d pwm:%g\r\n", servoIndex, servos[servoIndex].getPwmUs());        
    }
    else if(strcmp(servoCommand, "reverse") == 0)
    {
        if(!checkParameterCount(cmdArgs.paramCount, 4, "servo"))
            return;
        servoIndex = atoi(cmdArgs.params[2]);
        reverse = atoi(cmdArgs.params[3]);
        servos[servoIndex].setReversed(reverse);
        mcfg.servo_reversed[servoIndex] = reverse;
        writeConfigToFlash();
    }
	else if(strcmp(servoCommand, "getState") == 0)
	{
        servoIndex = atoi(cmdArgs.params[2]);
        printf("servoState index:%d pwm:%g angle:%g\r\n", servoIndex, servos[servoIndex].getPwmUs(), servos[servoIndex].getPosition());
	}
	else if(strcmp(servoCommand, "getPwm") == 0)
	{
        servoIndex = atoi(cmdArgs.params[2]);
        printf("servoState index:%d pwm:%g\r\n", servoIndex, servos[servoIndex].getPwmUs());
	}
	else if(strcmp(servoCommand, "getAngle") == 0)
	{
        servoIndex = atoi(cmdArgs.params[2]);
        printf("servoState index:%d angle:%g\r\n", servoIndex, servos[servoIndex].getPosition());
	}

	/*else if(strcmp(servoCommand, "get") == 0)
	{
        servoIndex = atoi(cmdArgs.params[3]);
		if(strcmp(cmdArgs.params[2], "ppm") == 0)
		{
			printf("pwm %d %g\r\n", servoIndex, servos[servoIndex].getPwmUs());
		} else if(strcmp(cmdArgs.params[2], "angle") == 0)
		{
			printf("angle %d %g\r\n", servoIndex, servos[servoIndex].getPosition());
		}
		
	}*/
    else if(strcmp(servoCommand, "help") == 0) 
    {
        printf("\r\n");
        printf("servo angle <index> <angle>\r\n");
        printf("servo pwm <index> <pulseWidth>\r\n");
        printf("servo calibrate <index> <range> <degree>\r\n");
        printf("servo subtrim <index> <increment>\r\n");
        printf("servo reverse <index> <reversed>\r\n");
        printf("servo getState <index>\r\n");		
        printf("servo getPwm <index>\r\n");		
        printf("servo getAngle <index>\r\n");		
        return;
    }
    else
    {
        printf("Syntax Error\r\n");
		return;
    }

    printf("OK\r\n");
}

void parseUnknown(CommandArgs cmdArgs)
{
    printf("Unknown command '%s'\r\n", cmdArgs.params[0]);
}

void parseCalibrate(CommandArgs cmdArgs)
{   
    if(strcmp(cmdArgs.params[1], "ppm") == 0)
    {
        current_activity = 1;
        ppm->ResetCalibration();
        printf("Move all sticks to maximum then type 'calibrate end'\r\n");
    } 
    else if(strcmp(cmdArgs.params[1], "end") == 0)
    {
        if(current_activity == 1) // End calibrate ppm
        {
                
            current_activity = -1;
            printf("Results:\r\n");
            for(int i = 0; i < ppm->GetNumChannel(); i++)
            {
                mcfg.ppm_maximumPulseTime[i] = ppm->maximumPulseTime[i];
                mcfg.ppm_minimumPulseTime[i] = ppm->minimumPulseTime[i];
                printf("Channel %d: min %d, max %d\r\n",i, ppm->minimumPulseTime[i], ppm->maximumPulseTime[i]);
            }
            writeConfigToFlash();
        }
    }
    else if(strcmp(cmdArgs.params[1], "help") == 0) 
    {
		printf("calibrate ppm\r\n");
		printf("calibrate end\r\n");
    }
    else 
    {
        printf("Syntax Error\r\n");
    }
}


void parseHelp(CommandArgs cmdArgs)
{
    commands.printCommandList();
}

void applyConfig()
{
    for(int i = 0; i < ppm->GetNumChannel(); i++)
    {
        ppm->maximumPulseTime[i] = mcfg.ppm_maximumPulseTime[i];
        ppm->minimumPulseTime[i] = mcfg.ppm_minimumPulseTime[i];
    }
    
    for(int i = 0; i < MAX_OUTPUT_CHANNELS; i++)
    {
        servos[i].setCenter(mcfg.servo_center[i]);
        servos[i].calibrate(mcfg.servo_calibrate_range[i], mcfg.servo_calibrate_degree[i]);
        servos[i].setReversed(mcfg.servo_reversed[i]);
    }
    for(int i = 0; i < MAX_OUTPUT_CHANNELS; i++)
    {
        servos[i].setPosition(0);
    }   
}


// ppm show
// ppm stop
// ppm disable
// ppm enable
void parsePPM(CommandArgs cmdArgs)
{
    if(strcmp(cmdArgs.params[1], "show") == 0)
	{
		current_activity = 2;
		printf("OK\r\n");
	}
	else if(strcmp(cmdArgs.params[1], "stop") == 0)
	{
		current_activity = -1;
		printf("OK\r\n");
	}
	else if(strcmp(cmdArgs.params[1], "disable") == 0)
	{
		enablePPM = false;
		printf("OK\r\n");
	}
	else if(strcmp(cmdArgs.params[1], "enable") == 0)
	{
		enablePPM = true;
		printf("OK\r\n");
	}
	else if(strcmp(cmdArgs.params[1], "help") == 0) 
    {
		printf("ppm show\r\n");
		printf("ppm stop\r\n");
		printf("ppm disable\r\n");
		printf("ppm enable\r\n");
    }
	else
	{
		printf("Syntax Error\r\n");		
	}

}

void showPPM()
{
    if(timer10.read_ms() >= 100)
    {
        timer10.reset();
        for(int i = 0; i < ppm->GetNumChannel(); i ++)
        {
            if(i != 0)
                printf(", ");
            printf("%.2f", ppm->GetData(i));
        }
        printf("\r\n");
    }           
}

void updateRobot()
{
    if(!enableMapping)
        return;
    if(currentChMapList != NULL)
    {
        for (int i = 0; i < (*currentChMapList).size(); i++) {
            ChannelMapping *chm = ((*currentChMapList)[i]);
            (chm->*(chm->Update))();
        }
    }
}

void parseHello(CommandArgs cmdArgs)
{
	initServoDriver();
    printf("Welcome\r\n");
}


// chMap ins <modeIndex> <ChMapType> <servo1>,<servo2>... <ch1>,<ch2>... <positionning>
// chMap upd <modeIndex> <ChMapType> <servo1>,<servo2>... <ch1>,<ch2>... <positionning> <chMapIndex>
// chMap del <modeIndex> <chMapIndex>
// chMap dump
// chMap dumpData <modeIndex>
// chMap setParams <modeIndex> <chMapIndex> <param1>,<param2>...
// chMap enable
// chMap disable
void parseChMap(CommandArgs cmdArgs)
{
    short modeIndex = atoi(cmdArgs.params[2]);
    if(modeIndex > MAX_MODE-1)
    {
        printf("Error: modeIndex too big\r\n");
        return;
    }
    bool insert = strcmp(cmdArgs.params[1], "ins") == 0;
    // Insert / Update
    if(strcmp(cmdArgs.params[1], "upd") == 0 || insert)
    {       
        int ChMapType = atoi(cmdArgs.params[3]);
        int positioning = atoi(cmdArgs.params[6]);
        ChannelMapping *chMap;
        if(insert) // Insert
        {
            chMap = new ChannelMapping(inputValues, servos);
        }
        else // Update
        {               
            int chMapIndex = atoi(cmdArgs.params[7]);
            if(chMapIndex > modeChMapList[modeIndex].chMapList.size()-1)
            {
                printf("Error: chMapIndex too big\r\n");
                return;
            }               
            chMap = modeChMapList[modeIndex].chMapList[chMapIndex];
        }
        chMap->Init(ChMapType, cmdArgs.params[4], cmdArgs.params[5], (PositioningType)positioning, 1);
        if(insert)
            modeChMapList[modeIndex].chMapList.push_back(chMap);
        saveChannelMappingList();
        printf("OK\r\n");
    }
    // Delete
    else if(strcmp(cmdArgs.params[1], "del") == 0)  
    {
        int chMapIndex = atoi(cmdArgs.params[3]);
        int size = modeChMapList[modeIndex].chMapList.size();
        if(chMapIndex > size-1)
        {
            printf("Error: chMapIndex too big\r\n");
            return;
        }
        modeChMapList[modeIndex].chMapList.erase(modeChMapList[modeIndex].chMapList.begin() + chMapIndex);
        saveChannelMappingList();
        printf("OK\r\n");
    }
    else if(strcmp(cmdArgs.params[1], "help") == 0)
    {
        printf("chMap ins <modeIndex> <ChMapType> <servo1>,<servo2>... <ch1>,<ch2>... <positionning>\r\n");
        printf("chMap upd <modeIndex> <ChMapType> <servo1>,<servo2>... <ch1>,<ch2>... <positionning> <chMapIndex>\r\n");
        printf("chMap del <modeIndex> <chMapIndex>\r\n");
        printf("chMap dump\r\n");
        printf("chMap dumpData <modeIndex>\r\n");
        printf("chMap setParams <modeIndex> <chMapIndex> <param1>,<param2>...\r\n");
        printf("chMap enable\r\n");
        printf("chMap disable\r\n");
        printf("\r\n");
		printf("ChMapType:\r\n");
		printf("1 - Direct\r\n");
		printf("2 - Tank Mix\r\n");
		printf("3 - IK Simple\r\n");
        printf("\r\n");
		printf("positionning:\r\n");
		printf("1 - Absolute\r\n");
		printf("2 - Relative	\r\n");	
    }
    else if(strcmp(cmdArgs.params[1], "dump") == 0)
    {
        dumpAllModeChMap();
    }
    else if(strcmp(cmdArgs.params[1], "dumpData") == 0)
    {       
        dumpModeChMapData(modeIndex);		
    }
    else if(strcmp(cmdArgs.params[1], "setParams") == 0)
    {
        int chMapIndex = atoi(cmdArgs.params[3]);
        int size = modeChMapList[modeIndex].chMapList.size();
        if(chMapIndex > size-1)
        {
            printf("Error: chMapIndex too big\r\n");
            return;
        }
        modeChMapList[modeIndex].chMapList[chMapIndex]->setParams(cmdArgs.params[4]);
        saveChannelMappingList();
        printf("OK\r\n");
    }
	else if(strcmp(cmdArgs.params[1], "enable") == 0)
	{
		enableMapping = true;
		printf("OK\r\n");
	}
	else if(strcmp(cmdArgs.params[1], "disable") == 0)
	{
		enableMapping = false;
        printf("OK\r\n");	}
    else
    {
        printf("Syntax Error\r\n");
    }
}

void checkChangeMode()  
{
    if(changeModeChIndex == -1)
	{
		currentChMapList = &modeChMapList[0].chMapList;
        return;
	}
    
    float chModeData = ppm->GetData(changeModeChIndex);
    for(int i = 0; i < MAX_MODE; i++)
    {
         if(chModeData >= modeChMapList[i].min && chModeData <= modeChMapList[i].max)
         {
             currentChMapList = &modeChMapList[i].chMapList;
             break;
         }
    }
}

// chMode range <modeIndex> <min> <max>
// chMode setChannel <chIndex>
void parseChMode(CommandArgs cmdArgs)
{
    if(strcmp(cmdArgs.params[1], "range") == 0)
    {
        int modeIndex = atoi(cmdArgs.params[2]);
        modeChMapList[modeIndex].min = atof(cmdArgs.params[3]);
        modeChMapList[modeIndex].max = atof(cmdArgs.params[4]);
        printf("OK\r\n");       
    }
    else if(strcmp(cmdArgs.params[1], "setChannel") == 0)
    {
        int channel = atoi(cmdArgs.params[2]);
        changeModeChIndex = channel;
        printf("OK\r\n");
    }
    else if(strcmp(cmdArgs.params[1], "dump") == 0)
    {
        dumpAllModeChMap();
    }   
    else if(strcmp(cmdArgs.params[1], "help") == 0)
    {
        printf("chMode range <modeIndex> <min> <max>\r\n");
        printf("chMode setChannel <chIndex>\r\n");
        printf("chMode dump\r\n");
    }else
    {
        printf("Syntax Error\r\n");
    }
    saveChannelMappingList();
}

// input <chIndex> <value>
void parseInput(CommandArgs cmdArgs)
{
	int channel = atoi(cmdArgs.params[1]);
	float value = atof(cmdArgs.params[2]);
	if(channel > MAX_INPUT_CHANNELS-1)
	{
		printf("Error: channel too big\r\n");
		return;
	}
	inputValues[channel] = value;
}

void updateInputValues()
{
	memcpy(inputValues, ppm->GetChannelDataAddr(), sizeof(float) * MAX_INPUT_CHANNELS);
}


int main()
{
    cmdIndex = 0;
    commandReady = 0;
    
    commands.add("help",parseHelp);
    commands.add("unknown",parseUnknown);
    commands.add("servo",parseServo);
    commands.add("calibrate",parseCalibrate);
    commands.add("ppm",parsePPM);
    commands.add("hello",parseHello);
    commands.add("chMap",parseChMap);
    commands.add("chMode",parseChMode);
    commands.add("input",parseInput);
	
	pc.baud (115200);
    pc.attach(&rxCallback, Serial::RxIrq);

	printf("Welcome\r\n");
    
    ppm = new PPM(PPM_INPUT_PIN, 0, 1, 1000, 2000, MAX_INPUT_CHANNELS, 3);
	inputValues = (float*)malloc(sizeof(float) * MAX_INPUT_CHANNELS);
	for(int i = 0; i < MAX_INPUT_CHANNELS; i++)
		inputValues[i] =  0.5;
	
    currentChMapList = NULL;

    initEEPROM();
    checkFirstTime(false);
    readConfigFromFlash();
    loadChannelMappingList(modeChMapList);
    
    applyConfig();
    /*if(changeModeChIndex == -1)
    {
        printf("Please specify change mode channel.\r\n");
    }*/
    initServoDriver();

    current_activity = -1;
    
    timer10.start();
    
    while(1)
    {
        if(commandReady)
        {
            commands.parseCommand(command);
            commandReady = 0;
        } 
        if(enablePPM)
		{
			ppm->UpdateChannelData();
			updateInputValues();
		}
        checkChangeMode();
        switch(current_activity)
        {
            case -1:
                updateRobot();
                break;
            case 1: 
                ppm->Calibrate();
                break;
            case 2:
                showPPM();
                break;
        }  
    }
}
