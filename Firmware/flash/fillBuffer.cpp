#include <math.h>
#include <stdlib.h>

#include "flash.h"
#include "string.h"
#include "fillbuffer.h"

/***********************************************************************************************/
// FillBuffer
// �crit dans le buffer jusqu'a ce qu'une page soit pleine et �crit la page dans la flash
// A �t� cr�� pour plus de performances (ErasePotData est intensif)
/***********************************************************************************************/


FillBuffer::FillBuffer()
{
	fillBufferOffset = 0;
}

void FillBuffer::InitFillBuffer(uint32_t flashAddress)
{

	fillBuffAddr = flashAddress;
	fillBufferOffset = 0;
	fillBuffer = malloc(FLASH_PAGE_SIZE);
}

// Il faut absolument que l'adresse de d�part soit au d�but d'une page
void FillBuffer::WriteFillBuffer()
{
	//FLASH_PageErase(fillBuffAddr);
	WriteDataToFlash(fillBuffer,fillBuffAddr, fillBufferOffset);
	fillBuffAddr += fillBufferOffset;
	fillBufferOffset = 0;
}

void FillBuffer::AddFillBuffer(void *data, uint16_t dataSize)
{
	int i;
	char *bFillBuffer = (char*)fillBuffer;
	for(i = 0; i < dataSize; i++)
	{
		bFillBuffer[fillBufferOffset++] = ((char*)data)[i];
		if(fillBufferOffset == FLASH_PAGE_SIZE)
		{
			WriteFillBuffer();
		}
	}
}

void FillBuffer::ReleaseFillBuffer()
{
	free(fillBuffer);
}
// Fillbuffer 2
// Plus lent un peu mais peut �crire � n'importe quelle adresse
/*void WriteFillBuffer2()
{
	WriteToFlash(fillBuffer, fillBuffAddr, fillBufferOffset);
	fillBuffAddr += fillBufferOffset;
	fillBufferOffset = 0;
}

void AddFillBuffer2(void *data, uint16_t dataSize)
{
	int i;
	char *bFillBuffer = (char*)fillBuffer;
	for(i = 0; i < dataSize; i++)
	{
		bFillBuffer[fillBufferOffset++] = ((char*)data)[i];
		if(fillBufferOffset == FLASH_PAGE_SIZE || (fillBuffAddr + fillBufferOffset) % FLASH_PAGE_SIZE == 0)
		{
			WriteFillBuffer2();
		}
	}
}*/

void* FillBuffer::ReadFillBuffer(uint16_t dataSize)
{
	ReadDataFromFlash(fillBuffer, fillBuffAddr, dataSize);
	fillBuffAddr += dataSize;
	return fillBuffer;
}

long FillBuffer::GetFillBufferAddr()
{
	return fillBuffAddr + fillBufferOffset;
}
