#include "ChannelMapping.h"	
#include "utils.h"
#include "math.h"
#include "flash.h"

ChannelMapping::ChannelMapping()
{
}

ChannelMapping::ChannelMapping(float* inputValues, Servo* servos)
{

	_inputValues = inputValues;
	_servos = servos;
	_channelIndex.clear();
	_multiplier = 1;
	_threshold = 0.01;
}

void ChannelMapping::UpdateDirect()
{
	if(_Positioning == ABSOLUTE)
		_servos[_servoIndex[0]].setPercent(_inputValues[_channelIndex[0]] * _multiplier);
	else if(_Positioning == RELATIVE)
	{
		float chValue = _inputValues[_channelIndex[0]] - 0.5;
		if(fabs(chValue) < _threshold)
			chValue = 0;
		_servos[_servoIndex[0]].setPosition(_servos[_servoIndex[0]].getPosition() + chValue * _multiplier);
	}
}

void ChannelMapping::UpdateTankMix()
{
	float angle1, angle2;
	angle1 = (0.5 - _inputValues[_channelIndex[0]]) - (_inputValues[_channelIndex[1]] - 0.5);
	angle2 = (0.5 - _inputValues[_channelIndex[0]]) + (_inputValues[_channelIndex[1]] - 0.5);
	angle1 *= _servos[_servoIndex[0]].getDegrees();
	angle2 *= _servos[_servoIndex[1]].getDegrees();
	_servos[_servoIndex[0]].setPosition(angle1 * _multiplier);
	_servos[_servoIndex[1]].setPosition(angle2 * _multiplier);
}

void ChannelMapping::UpdateIK()
{
	float len1 = _params[0];
	float len2 = _params[1];
	
	// Update claw angle
	float chValue = _inputValues[_channelIndex[2]] - 0.5;
	if(_Positioning == ABSOLUTE)
		angleClawWorld = chValue * 180;
	else if(_Positioning == RELATIVE)
	{		
		if(fabs(chValue) < _threshold)
			chValue = 0;
		angleClawWorld += chValue;
	}
	
	// Update targetX
	float chValue1 = _inputValues[_channelIndex[0]] - 0.5;
	if(fabs(chValue1) < 0.01)
		chValue1 = 0;
	targetX += chValue1;

	// Update targetY
	float chValue2 = _inputValues[_channelIndex[1]] - 0.5;
	if(fabs(chValue2) < 0.01)
		chValue2 = 0;
	targetY += chValue2 / 2;
			
	float dTarget = sqrt(targetX * targetX + targetY * targetY); // Lenght of target vector
	float aTarget = RAD2DEG * atan2(targetY, targetX); // Angle of target vector
	volatile float angleSoulder = aTarget + RAD2DEG * GetTriangleAngle2(len2, len1,dTarget);
	volatile float angleElbow = RAD2DEG * GetTriangleAngle2(dTarget, len2, len1);
	angleElbow = 180.0 - angleElbow;
	volatile float clawAngle = angleClawWorld - (angleSoulder - angleElbow);
	_servos[_servoIndex[0]].setPosition(angleSoulder);
	_servos[_servoIndex[1]].setPosition(angleElbow);
	_servos[_servoIndex[2]].setPosition(clawAngle);
}


void stringToShortVector(char *str, vector<short>& lst)
{
	int count;
	char** strArr = str_split(str,',', count);
	lst.clear();
	for(int i = 0; i < count; i++)
	{
		lst.push_back(atoi(strArr[i]));
		free(strArr[i]);
	}
	free(strArr);
}

void stringToFloatVector(char *str, vector<float>& lst)
{
	int count;
	char** strArr = str_split(str,',', count);
	lst.clear();
	for(int i = 0; i < count; i++)
	{
		lst.push_back(atof(strArr[i]));
		free(strArr[i]);
	}
	free(strArr);
}

void ChannelMapping::Init(short chMapType, char* servoIndexLst, char* chIndexLst, PositioningType positioning, float multiplier)
{
	stringToShortVector(servoIndexLst, _servoIndex);
	stringToShortVector(chIndexLst, _channelIndex);
	_Positioning = positioning;
	_multiplier = multiplier;
	_chMapType = chMapType;
	if(chMapType == DIRECT)
		Update = &ChannelMapping::UpdateDirect;
	else if(chMapType == TANK_MIX)
		Update = &ChannelMapping::UpdateTankMix;
	else if(chMapType == IK_SIMPLE)
	{
		Update = &ChannelMapping::UpdateIK;
		targetX = 100;
		targetY = 20;
	}
}

void ChannelMapping::Save(FillBuffer &fillBuffer)
{
	fillBuffer.AddFillBuffer(&_Positioning, sizeof(PositioningType));
	fillBuffer.AddFillBuffer(&_multiplier, sizeof(float));
	fillBuffer.AddFillBuffer(&_chMapType, sizeof(short));

	int lstSize;
	lstSize = _servoIndex.size();
	fillBuffer.AddFillBuffer(&lstSize, sizeof(int));
	for(int i = 0; i < _servoIndex.size(); i++)
	{
		fillBuffer.AddFillBuffer(&_servoIndex[i], sizeof(short));
	}

	lstSize = _channelIndex.size();
	fillBuffer.AddFillBuffer(&lstSize, sizeof(int));
	for(int i = 0; i < _channelIndex.size(); i++)
	{
		fillBuffer.AddFillBuffer(&_channelIndex[i], sizeof(short));
	}

	lstSize = _params.size();
	fillBuffer.AddFillBuffer(&lstSize, sizeof(int));
	for(int i = 0; i < _params.size(); i++)
	{
		fillBuffer.AddFillBuffer(&_params[i], sizeof(float));
	}

}

void ChannelMapping::Load(FillBuffer &fillBuffer)
{
	_Positioning = (*(PositioningType*)fillBuffer.ReadFillBuffer(sizeof(_Positioning)));
	_multiplier = (*(float*)fillBuffer.ReadFillBuffer(sizeof(float)));
	_chMapType = (*(short*)fillBuffer.ReadFillBuffer(sizeof(short)));

	if(_chMapType == DIRECT)
		Update = &ChannelMapping::UpdateDirect;
	else if(_chMapType == TANK_MIX)
		Update = &ChannelMapping::UpdateTankMix;
	else if(_chMapType == IK_SIMPLE)
	{
		Update = &ChannelMapping::UpdateIK;
		targetX = 100;
		targetY = 20;
	}	
	int lstSize;
	lstSize = (*(int*)fillBuffer.ReadFillBuffer(sizeof(int)));
	for(int i = 0; i < lstSize; i++)
	{
		short data = (*(short*)fillBuffer.ReadFillBuffer(sizeof(short)));
		_servoIndex.push_back(data);
	}
	
	lstSize = (*(int*)fillBuffer.ReadFillBuffer(sizeof(int)));
	for(int i = 0; i < lstSize; i++)
	{
		short data = (*(short*)fillBuffer.ReadFillBuffer(sizeof(short)));
		_channelIndex.push_back(data);
	}	

	lstSize = (*(int*)fillBuffer.ReadFillBuffer(sizeof(int)));
	for(int i = 0; i < lstSize; i++)
	{
		float data = (*(float*)fillBuffer.ReadFillBuffer(sizeof(float)));
		_params.push_back(data);
	}	
}

void ChannelMapping::dump()
{
	const char* chMapTypeName[] = {"Unknown","Direct","Tank-mix","IK"};
	
	printf("    Type: %s\r\n",_chMapType < 4 ? chMapTypeName[_chMapType] : chMapTypeName[0]);
	printf("    Positioning: %s\r\n",_Positioning==1?"Absolute":_Positioning==2?"Relative":"NA");
	//printf("    Multiplier: %g\r\n",_multiplier);

	printf("    Servo Index(es):");
	for(int i = 0; i < _servoIndex.size(); i++)
	{
		if(i > 0) printf(",");
		printf("%d",_servoIndex[i]);
	}
	printf("\r\n");

	printf("    Channel Index(es):");
	for(int i = 0; i < _channelIndex.size(); i++)
	{
		if(i > 0) printf(",");
		printf("%d",_channelIndex[i]);
	}
	printf("\r\n");

	if(_params.size() > 0)
	{
		printf("    Params:");
		for(int i = 0; i < _params.size(); i++)
		{
			if(i > 0) printf(",");
			printf("%g",_params[i]);
		}
		printf("\r\n");
	}
}

// chMap ins <modeIndex> <ChMapType> <servo1>,<servo2>... <ch1>,<ch2>... <positionning>
// chMap setParams <modeIndex> <chMapIndex> <param1>,<param2>...
void ChannelMapping::dumpData(int modeIndex, int chMapIndex)
{
	printf("chMap ins ");
	printf("%d ",modeIndex);

	printf("%d ", _chMapType);
	for(int i = 0; i < _servoIndex.size(); i++)
	{
		if(i > 0) printf(",");
		printf("%d",_servoIndex[i]);
	}
	printf(" ");
	for(int i = 0; i < _channelIndex.size(); i++)
	{
		if(i > 0) printf(",");
		printf("%d",_channelIndex[i]);
	}
	printf(" %d", _Positioning);	
	
	// print parameters
	if(_params.size() > 0)
	{
		printf("\r\n");
		printf("chMap setParams ");
		printf("%d ",modeIndex);
		printf("%d ", chMapIndex);
		for(int i = 0; i < _params.size(); i++)
		{
			if(i > 0) printf(",");
			printf("%g",_params[i]);
		}
	}
    printf("\r\n");

}

void ChannelMapping::setParams(char* paramLst)
{
	stringToFloatVector(paramLst, _params);
}
