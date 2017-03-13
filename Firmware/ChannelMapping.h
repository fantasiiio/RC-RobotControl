#pragma once
#include "servo.h"
#include "PPM.h"
#include <vector>
#include "fillBuffer.h"

enum PositioningType
{
	ABSOLUTE = 1,
	RELATIVE = 2
};

enum ChMapType
{
	DIRECT = 1,
	TANK_MIX = 2,
	IK_SIMPLE = 3
};

class ChannelMapping
{	
public:
	ChannelMapping();
	ChannelMapping(float* inputValues, Servo* servos);
	void Init(short ChMapType, char* servoIndexLst, char* chIndexLst, PositioningType positioning, float multiplier);
	void(ChannelMapping::*Update)();
	void Save(FillBuffer &fillBuffer);
	void Load(FillBuffer &fillBuffer);
	void dump();
	void dumpData(int modeIndex, int chMapIndex);
	void setParams(char* paramLst);
private:
	short _chMapType;
	float _threshold;
	Servo *_servos;
	//PPM *_ppm;
	float* _inputValues;
	vector<short> _servoIndex;
	vector<short> _channelIndex;
	vector<float> _params;
	float _multiplier;
	PositioningType _Positioning;
	float targetX, targetY, angleClawWorld; // IK stuff

	void UpdateDirect();
	void UpdateTankMix();
	void UpdateIK();
	void UpdateValue();
};
