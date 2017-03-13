#pragma once

class FillBuffer
{
public:
	FillBuffer();
	void InitFillBuffer(uint32_t flashAddress);
	void WriteFillBuffer(void);
	void AddFillBuffer(void *data, uint16_t dataSize);
	void* ReadFillBuffer(uint16_t dataSize);
	void ReleaseFillBuffer(void);
	void WriteFillBuffer2(void);
	void AddFillBuffer2(void *data, uint16_t dataSize);
	long GetFillBufferAddr(void);

private:
	uint32_t fillBuffAddr;
	int fillBufferOffset;
	void *fillBuffer;	
};

