#include "config.h"
#include "flash.h"
#include <string.h> /* memset */
#include "math.h"

#define ASSERT_CONCAT_(a, b) a##b
#define ASSERT_CONCAT(a, b) ASSERT_CONCAT_(a, b)
#define ct_assert(e) enum { ASSERT_CONCAT(assert_line_, __LINE__) = 1/(!!(e)) }

// define this symbol to increase or decrease flash size. not rely on flash_size_register.
#ifndef FLASH_PAGE_COUNT
#define FLASH_PAGE_COUNT (*((uint16_t *)FLASH_SIZE_DATA_REGISTER)) 
//128
#endif

//#define FLASH_PAGE_SIZE                 ((uint16_t)0x400)
// if sizeof(mcfg) is over this number, compile-time error will occur. so, need to add another page to config data.
#define CONFIG_SIZE                     (FLASH_PAGE_SIZE * 2)

static const uint8_t EEPROM_CONF_VERSION = 5;
const uint32_t FLASH_WRITE_ADDR = 0x08000000 + (FLASH_PAGE_SIZE * (FLASH_PAGE_COUNT - (CONFIG_SIZE / FLASH_PAGE_SIZE))); //reserve 2k
const uint32_t FLASH_UPLOAD_ADDR = FLASH_WRITE_ADDR - (FLASH_PAGE_SIZE * 2); //reserve 2k
const uint32_t FLASH_CHANNEL_MAPPING_ADDR = FLASH_UPLOAD_ADDR - (FLASH_PAGE_SIZE); //reserve 1k

#define     __IO    volatile                  /*!< defines 'read / write' permissions   */

/*typedef struct
{
  __IO uint32_t ACR;
  __IO uint32_t KEYR;
  __IO uint32_t OPTKEYR;
  __IO uint32_t SR;
  __IO uint32_t CR;
  __IO uint32_t AR;
  __IO uint32_t RESERVED;
  __IO uint32_t OBR;
  __IO uint32_t WRPR;
} FLASH_TypeDef;*/

#define FLASH               ((FLASH_TypeDef *) FLASH_R_BASE)

//#define PERIPH_BASE           ((uint32_t)0x40000000) /*!< Peripheral base address in the alias region */
//#define AHBPERIPH_BASE        (PERIPH_BASE + 0x20000)
#define FLASH_R_BASE          (AHBPERIPH_BASE + 0x2000) /*!< Flash registers base address */

#define FLASH_KEY1               ((uint32_t)0x45670123)
#define FLASH_KEY2               ((uint32_t)0xCDEF89AB)

//#define FLASH_FLAG_BSY                 ((uint32_t)0x00000001)  /*!< FLASH Busy flag */
//#define FLASH_FLAG_EOP                 ((uint32_t)0x00000020)  /*!< FLASH End of Operation flag */
//#define FLASH_FLAG_PGERR               ((uint32_t)0x00000004)  /*!< FLASH Program error flag */
#define FLASH_FLAG_WRPRTERR            ((uint32_t)0x00000010)  /*!< FLASH Write protected error flag */
#define FLASH_FLAG_OPTERR              ((uint32_t)0x00000001)  /*!< FLASH Option Byte error flag */

#define EraseTimeout          ((uint32_t)0x000B0000)
#define ProgramTimeout        ((uint32_t)0x00002000)

/* Flash Control Register bits */
#define CR_PG_Set                ((uint32_t)0x00000001)
#define CR_PG_Reset              ((uint32_t)0x00001FFE) 
#define CR_PER_Set               ((uint32_t)0x00000002)
#define CR_PER_Reset             ((uint32_t)0x00001FFD)
#define CR_MER_Set               ((uint32_t)0x00000004)
#define CR_MER_Reset             ((uint32_t)0x00001FFB)
#define CR_OPTPG_Set             ((uint32_t)0x00000010)
#define CR_OPTPG_Reset           ((uint32_t)0x00001FEF)
#define CR_OPTER_Set             ((uint32_t)0x00000020)
#define CR_OPTER_Reset           ((uint32_t)0x00001FDF)
#define CR_STRT_Set              ((uint32_t)0x00000040)
#define CR_LOCK_Set              ((uint32_t)0x00000080)

master_t mcfg;   // profile config struct

static void resetConf(void);

void initEEPROM(void)
{
    // make sure (at compile time) that config struct doesn't overflow allocated flash pages
    ct_assert(sizeof(mcfg) < CONFIG_SIZE);
}

static uint8_t validEEPROM(void)
{
    const master_t *temp = (const master_t *)FLASH_WRITE_ADDR;
    const uint8_t *p;
    uint8_t chk = 0;

    // check version number
    if (EEPROM_CONF_VERSION != temp->version)
        return 0;

    // check size and magic numbers
    if (temp->size != sizeof(master_t) || temp->magic_be != 0xBE || temp->magic_ef != 0xEF)
        return 0;

    // verify integrity of temporary copy
    for (p = (const uint8_t *)temp; p < ((const uint8_t *)temp + sizeof(master_t)); p++)
        chk ^= *p;

    // checksum failed
    if (chk != 0)
        return 0;

    // looks good, let's roll!
    return 1;
}

int readConfigFromFlash(void)
{
    // Sanity check
    if (!validEEPROM())
        return 0;

    // Read flash
    memcpy(&mcfg, (char *)FLASH_WRITE_ADDR, sizeof(master_t));
    return 1;
}

/**
  * @brief  Unlocks the FLASH Program Erase Controller.
  * @note   This function can be used for all STM32F10x devices.
  *         - For STM32F10X_XL devices this function unlocks Bank1 and Bank2.
  *         - For all other devices it unlocks Bank1 and it is equivalent 
  *           to FLASH_UnlockBank1 function.. 
  * @param  None
  * @retval None
  */
void FLASH_Unlock(void)
{
  /* Authorize the FPEC of Bank1 Access */
  FLASH->KEYR = FLASH_KEY1;
  FLASH->KEYR = FLASH_KEY2;
}

/**
  * @brief  Clears the FLASH's pending flags.
  * @note   This function can be used for all STM32F10x devices.
  *         - For STM32F10X_XL devices, this function clears Bank1 or Bank2’s pending flags
  *         - For other devices, it clears Bank1’s pending flags.
  * @param  FLASH_FLAG: specifies the FLASH flags to clear.
  *   This parameter can be any combination of the following values:         
  *     @arg FLASH_FLAG_PGERR: FLASH Program error flag       
  *     @arg FLASH_FLAG_WRPRTERR: FLASH Write protected error flag      
  *     @arg FLASH_FLAG_EOP: FLASH End of Operation flag           
  * @retval None
  */
void FLASH_ClearFlag(uint32_t FLASH_FLAG)
{
  /* Clear the flags */
  FLASH->SR = FLASH_FLAG;
}



/*typedef enum
{ 
  HAL_BUSY = 1,
  HAL_ERROR,
  HAL_ERROR,
  HAL_OK,
  HAL_TIMEOUT
}FLASH_Status;*/

#define FLASH_FLAG_BANK1_BSY                 FLASH_FLAG_BSY       /*!< FLASH BANK1 Busy flag*/
#define FLASH_FLAG_BANK1_EOP                 FLASH_FLAG_EOP       /*!< FLASH BANK1 End of Operation flag */
#define FLASH_FLAG_BANK1_PGERR               FLASH_FLAG_PGERR     /*!< FLASH BANK1 Program error flag */
#define FLASH_FLAG_BANK1_WRPRTERR            FLASH_FLAG_WRPRTERR  /*!< FLASH BANK1 Write protected error flag */


/**
  * @brief  Returns the FLASH Bank1 Status.
  * @note   This function can be used for all STM32F10x devices, it is equivalent
  *         to FLASH_GetStatus function.
  * @param  None
  * @retval FLASH Status: The returned value can be: HAL_BUSY, HAL_ERROR,
  *         HAL_ERROR or HAL_OK
  */
HAL_StatusTypeDef FLASH_GetBank1Status(void)
{
  HAL_StatusTypeDef flashstatus = HAL_OK;
  
  if((FLASH->SR & FLASH_FLAG_BANK1_BSY) == FLASH_FLAG_BSY) 
  {
    flashstatus = HAL_BUSY;
  }
  else 
  {  
    if((FLASH->SR & FLASH_FLAG_BANK1_PGERR) != 0)
    { 
      flashstatus = HAL_ERROR;
    }
    else 
    {
      if((FLASH->SR & FLASH_FLAG_BANK1_WRPRTERR) != 0 )
      {
        flashstatus = HAL_ERROR;
      }
      else
      {
        flashstatus = HAL_OK;
      }
    }
  }
  /* Return the Flash Status */
  return flashstatus;
}

/**
  * @brief  Waits for a Flash operation to complete or a TIMEOUT to occur.
  * @note   This function can be used for all STM32F10x devices, 
  *         it is equivalent to FLASH_WaitForLastBank1Operation.
  *         - For STM32F10X_XL devices this function waits for a Bank1 Flash operation
  *           to complete or a TIMEOUT to occur.
  *         - For all other devices it waits for a Flash operation to complete 
  *           or a TIMEOUT to occur.
  * @param  Timeout: FLASH programming Timeout
  * @retval FLASH Status: The returned value can be: HAL_ERROR,
  *         HAL_ERROR, HAL_OK or HAL_TIMEOUT.
  */
HAL_StatusTypeDef FLASH_WaitForLastOperation_(uint32_t Timeout)
{ 
  HAL_StatusTypeDef status = HAL_OK;
   
  /* Check for the Flash Status */
  status = FLASH_GetBank1Status();
  /* Wait for a Flash operation to complete or a TIMEOUT to occur */
  while((status == HAL_BUSY) && (Timeout != 0x00))
  {
    status = FLASH_GetBank1Status();
    Timeout--;
  }
  if(Timeout == 0x00 )
  {
    status = HAL_TIMEOUT;
  }
  /* Return the operation status */
  return status;
}

//HAL_StatusTypeDef FLASH_WaitForLastOperation_(uint32_t Timeout);

/**
  * @brief  Erases a specified FLASH page.
  * @note   This function can be used for all STM32F10x devices.
  * @param  Page_Address: The page address to be erased.
  * @retval FLASH Status: The returned value can be: HAL_BUSY, HAL_ERROR,
  *         HAL_ERROR, HAL_OK or HAL_TIMEOUT.
  */
HAL_StatusTypeDef FLASH_ErasePage(uint32_t Page_Address)
{
  HAL_StatusTypeDef status = HAL_OK;

  /* Wait for last operation to be completed */
  status = FLASH_WaitForLastOperation_(EraseTimeout);
  
  if(status == HAL_OK)
  { 
    /* if the previous operation is completed, proceed to erase the page */
    FLASH->CR|= CR_PER_Set;
    FLASH->AR = Page_Address; 
    FLASH->CR|= CR_STRT_Set;
    
    /* Wait for last operation to be completed */
    status = FLASH_WaitForLastOperation_(EraseTimeout);
    
    /* Disable the PER Bit */
    FLASH->CR &= CR_PER_Reset;
  }

  /* Return the Erase Status */
  return status;
}



/**
  * @brief  Programs a word at a specified address.
  * @note   This function can be used for all STM32F10x devices.
  * @param  Address: specifies the address to be programmed.
  * @param  Data: specifies the data to be programmed.
  * @retval FLASH Status: The returned value can be: HAL_ERROR,
  *         HAL_ERROR, HAL_OK or HAL_TIMEOUT. 
  */
HAL_StatusTypeDef FLASH_ProgramWord(uint32_t Address, uint32_t Data)
{
  HAL_StatusTypeDef status = HAL_OK;
  __IO uint32_t tmp = 0;

  /* Wait for last operation to be completed */
  status = FLASH_WaitForLastOperation_(ProgramTimeout);
  
  if(status == HAL_OK)
  {
    /* if the previous operation is completed, proceed to program the new first 
    half word */
    FLASH->CR |= CR_PG_Set;
  
    *(__IO uint16_t*)Address = (uint16_t)Data;
    /* Wait for last operation to be completed */
    status = FLASH_WaitForLastOperation_(ProgramTimeout);
 
    if(status == HAL_OK)
    {
      /* if the previous operation is completed, proceed to program the new second 
      half word */
      tmp = Address + 2;

      *(__IO uint16_t*) tmp = Data >> 16;
    
      /* Wait for last operation to be completed */
      status = FLASH_WaitForLastOperation_(ProgramTimeout);
        
      /* Disable the PG Bit */
      FLASH->CR &= CR_PG_Reset;
    }
    else
    {
      /* Disable the PG Bit */
      FLASH->CR &= CR_PG_Reset;
    }
  }         
  /* Return the Program Status */
  return status;
}

/**
  * @brief  Locks the FLASH Program Erase Controller.
  * @note   This function can be used for all STM32F10x devices.
  *         - For STM32F10X_XL devices this function Locks Bank1 and Bank2.
  *         - For all other devices it Locks Bank1 and it is equivalent 
  *           to FLASH_LockBank1 function.
  * @param  None
  * @retval None
  */
void FLASH_Lock(void)
{
  /* Set the Lock Bit to lock the FPEC and the CR of  Bank1 */
  FLASH->CR |= CR_LOCK_Set;

}

int writeConfigToFlash()
{
    HAL_StatusTypeDef status = HAL_OK;
    uint8_t chk = 0;
    const uint8_t *p;

    // prepare checksum/version constants
    mcfg.version = EEPROM_CONF_VERSION;
    mcfg.size = sizeof(master_t);
    mcfg.magic_be = 0xBE;
    mcfg.magic_ef = 0xEF;
    mcfg.chk = 0;

    // recalculate checksum before writing
    for (p = (const uint8_t *)&mcfg; p < ((const uint8_t *)&mcfg + sizeof(master_t)); p++)
        chk ^= *p;
    mcfg.chk = chk;

    // write it
    FLASH_Unlock();
    for (unsigned int tries = 3; tries; tries--) {
        FLASH_ClearFlag(FLASH_FLAG_EOP | FLASH_FLAG_PGERR | FLASH_FLAG_WRPRTERR);

        FLASH_ErasePage(FLASH_WRITE_ADDR);
        FLASH_ErasePage(FLASH_WRITE_ADDR + FLASH_PAGE_SIZE);
        for (unsigned int i = 0; i < sizeof(master_t) && status == HAL_OK; i += 4)
            status = FLASH_ProgramWord(FLASH_WRITE_ADDR + i, *(uint32_t *)((char *)&mcfg + i));
        if (status == HAL_OK)
            break;                    
    }
    FLASH_Lock();

    // Flash write failed - just die now
    if (status != HAL_OK || !validEEPROM()) {
        return 0;
    }
    return 1;
}

int WriteDataToFlash(void *data, uint32_t Page_Address, uint32_t size)
{
	int numPages, i;
    HAL_StatusTypeDef status = HAL_OK;
	
	numPages = ceil((float)size / (float)FLASH_PAGE_SIZE);	
	
	FLASH_Unlock();
    for (unsigned int tries = 3; tries; tries--) {
        FLASH_ClearFlag(FLASH_FLAG_EOP | FLASH_FLAG_PGERR | FLASH_FLAG_WRPRTERR);

		for(i = 0; i < numPages; i++)
			FLASH_ErasePage(Page_Address + FLASH_PAGE_SIZE * i);
        
        for (unsigned int i = 0; i < size && status == HAL_OK; i += 4)
            status = FLASH_ProgramWord(Page_Address + i, *(uint32_t *)((char *)data + i));
        if (status == HAL_OK)
            break;                    
    }
    FLASH_Lock();
		
	    // Flash write failed - just die now
    if (status != HAL_OK || !validEEPROM()) {
        return 0;
    }
    return 1;
}



void ReadDataFromFlash(void *data, uint32_t Page_Address, uint32_t size)
{
    // Read flash
    memcpy(data, (char *)Page_Address, size);
}

// Default settings
static void resetConf(void)
{
    // Clear all configuration
    memset(&mcfg, 0, sizeof(master_t));
    
    mcfg.version = EEPROM_CONF_VERSION;  
	for(int i = 0; i < MAX_OUTPUT_CHANNELS; i++)
	{
		mcfg.servo_center[i] = 0.0015;
		mcfg.servo_calibrate_degree[i] = 90;
		mcfg.servo_calibrate_range[i] = 0.001;
		mcfg.servo_reversed[i] = false;	
	}
	
	for(int i = 0; i < MAX_INPUT_CHANNELS; i++)
	{
		mcfg.ppm_maximumPulseTime[i] = 2000;
		mcfg.ppm_minimumPulseTime[i] = 1000;
	}

	for(int i = 0; i < MAX_OUTPUT_CHANNELS; i++)
	{
		mcfg.servo_center[i] = 0.0015f;
	}
}

void checkFirstTime(int reset)
{
    // check the EEPROM integrity before resetting values
    if (!validEEPROM() || reset) {
        resetConf();
        // no need to memcpy profile again, we just did it in resetConf() above
        writeConfigToFlash();
    }
}

