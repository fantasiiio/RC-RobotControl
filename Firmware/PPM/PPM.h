#include "mbed.h"
#include "config.h"

#ifndef PPM_H
#define PPM_H


class PPM
{
    public:
        //Constructor
        PPM(PinName pin, float minimumOutput, float maximumOutput, int minimumPulseTime, int maximumPulseTime, int numberOfChannels, int throttleChannel);
		~PPM();
    private:
        //Interrupt
        void SignalRise();
        
        //Interrupt pin
        InterruptIn *_ppmPin;
        
        //Timer, times length of pulses
        Timer _timer;
        //Number of channels in PPM signal
        int _numberOfChannels;
        //Current channel
        char _currentChannel;  
        //Stores channel times
        int _times[100];
        //Stores most recent complete frame times
        int _completeTimes[100];
        //Keeps track of time between PPM interrupts
        int _timeElapsed; 
        //Minimum time of frame
        int _minFrameTime;
        //If the pulse time for a channel is this short, something is wrong uS
        int _shortTime;
        //Minimum output
        float _minimumOutput;
        //Maximum output
        float _maximumOutput;
        //Throttle channel - used for fail safe
        int _throttleChannel;
		//Array to hold RC commands
		float _channelData[MAX_INPUT_CHANNELS];
        
    public:
        //Minimum pulse time uS
        int minimumPulseTime[MAX_INPUT_CHANNELS];
        //Maximum pulse time uS
        int maximumPulseTime[MAX_INPUT_CHANNELS];
	
        //Update channel data
        void UpdateChannelData();
		//Calibrate all channel
		void Calibrate();
		//Get channel data
		float GetData(int channel);
		float* GetChannelDataAddr();
		
		int GetNumChannel();
		//int GetMinimumPulseTime(int channel);
		//int GetMaximumPulseTime(int channel);
		void ResetCalibration();

};

#endif


