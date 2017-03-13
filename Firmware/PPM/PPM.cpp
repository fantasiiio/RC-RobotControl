#include "PPM.h"    

//PPM reader by Joe Roberts, based on work by John Wolter
//This program takes the PPM Signal (Pulse Position Modulation) from your RC transmitter and outputs the data between the min/max outputs passed into the constructor
//See 
PPM::PPM(PinName pin, float minimumOutput, float maximumOutput, int minimumPulseTime_, int maximumPulseTime_, int numberOfChannels, int throttleChannel) 
{    
    //Assign local variables passed into constructor
    _ppmPin = new InterruptIn(pin);
	_minimumOutput = minimumOutput;
	_maximumOutput = maximumOutput;	

	if(numberOfChannels > MAX_OUTPUT_CHANNELS)
		numberOfChannels = MAX_OUTPUT_CHANNELS;	
    _numberOfChannels = numberOfChannels;
    _throttleChannel = throttleChannel;
        
    //Set other variables
    _currentChannel = 0;  
    _timeElapsed = 0; 
    _minFrameTime = 6000;
    _shortTime = 800;

    //Initialise arrays
    for(int i = 0; i < _numberOfChannels; i++)
    {
		this->minimumPulseTime[i] = minimumPulseTime_;
		this->maximumPulseTime[i] = maximumPulseTime_;
		_channelData[i] = 0;
        _times[i] = 0;
        _completeTimes[i] = 0;
    }
    
    //Assign interrupt
    _ppmPin->mode (PullUp);
    _ppmPin->rise (this, &PPM::SignalRise);

    //Start timer
    _timer.start();
}
   
PPM::~PPM()
{
	delete _ppmPin;
}

//Here is where all the work decoding the PPM signal takes place
void PPM::SignalRise()
{
    //Get the time taken since the last interrupt
    _timeElapsed = _timer.read_us();
    
    //If time is less than _shortTime then the channel timing is too short - ignore
    if (_timeElapsed < _shortTime) return;
    
    //Disable the interrupt
    _ppmPin->rise(NULL);

    //Reset the timer
    _timer.reset();
    
    //Check for a new frame signal, if before start of new frame then its a glitch - start a new frame
    if ((_timeElapsed > _minFrameTime) && (_currentChannel != 0)) _currentChannel = 0;
    
    //Check for a new frame signal, if it is the start of a new frame then start new frame
    if ((_timeElapsed > _minFrameTime ) && (_currentChannel == 0))
    {
        //Assign interrupt
        _ppmPin->rise (this, &PPM::SignalRise);
        return;
    }
 
    //Save the time to the times array
    _times[_currentChannel] = _timeElapsed;
    _currentChannel++;
    
    //Check for a complete frame
    if (_currentChannel == _numberOfChannels)
    {
        //Set channel iterator to 0
        _currentChannel = 0;
        //Copy times array to complete times array
        memcpy(_completeTimes, _times, sizeof(_times));
    }

    //Assign interrupt
    _ppmPin->rise(this, &PPM::SignalRise);
    return;
}

//Place mapped channel data into the passed in array
void PPM::UpdateChannelData()
{
    //Iterate over the channel times array
    for(int i = 0; i < _numberOfChannels; i++)
    {
        //Check the transmitter is still connected by checking the thottle
        if((i == _throttleChannel - 1) && (_completeTimes[i] < minimumPulseTime[i]))
			_channelData[i] = -1;
        else
        {
            //Map the channel times to value between the channel min and channel max
            _channelData[i] = (_completeTimes[i] - minimumPulseTime[i]) * (_maximumOutput - _minimumOutput) / (maximumPulseTime[i] - minimumPulseTime[i]) + _minimumOutput;
        }
    }
    
    return; 
}

void PPM::ResetCalibration()
{
	for(int i = 0; i < _numberOfChannels; i++)
	{
		minimumPulseTime[i] = 9999;
		maximumPulseTime[i] = -1;
	}
}

void PPM::Calibrate()
{
	for(int i = 0; i < _numberOfChannels; i++)
	{
		if(_completeTimes[i] < minimumPulseTime[i])
			minimumPulseTime[i] = _completeTimes[i];
		
		if(_completeTimes[i] > maximumPulseTime[i])
			maximumPulseTime[i] = _completeTimes[i];
	}
}

float PPM::GetData(int channel)
{
	return this->_channelData[channel];
}

float* PPM::GetChannelDataAddr()
{
	return this->_channelData;
}

int PPM::GetNumChannel()
{
	return this->_numberOfChannels;
}

/*int PPM::GetMinimumPulseTime(int channel)
{
	return this->minimumPulseTime[channel];
}
int PPM::GetMaximumPulseTime(int channel)
{
	return this->maximumPulseTime[channel];
}*/

