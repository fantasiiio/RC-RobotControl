#pragma once
#include "stdint.h"
#include "config.h"
#include "stm32f1xx.h"
//#include "cmsis.h"
//#define FLASH_PAGE_SIZE                 ((uint16_t)0x400)

extern const uint32_t FLASH_WRITE_ADDR; 
extern const uint32_t FLASH_UPLOAD_ADDR;
extern const uint32_t FLASH_CHANNEL_MAPPING_ADDR;
/*typedef enum
{ 
  FLASH_BUSY_ = 1,
  FLASH_ERROR_PG_,
  FLASH_ERROR_WRP_,
  FLASH_COMPLETE_,
  FLASH_TIMEOUT_
}FLASH_Status;*/

void initEEPROM(void);
int writeConfigToFlash(void);
void checkFirstTime(int reset);
int readConfigFromFlash(void);
int WriteDataToFlash(void *data, uint32_t Page_Address, uint32_t size);
void ReadDataFromFlash(void *data, uint32_t Page_Address, uint32_t size);



//FLASH_Status FLASH_ErasePage(uint32_t Page_Address);
