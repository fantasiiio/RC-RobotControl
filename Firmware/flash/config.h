#pragma once
#include "stdint.h"
#include "stdbool.h"

#define MAX_INPUT_CHANNELS 8
#define MAX_OUTPUT_CHANNELS 16

typedef struct master_t {
    uint8_t version;
    uint16_t size;
    uint8_t magic_be;  // magic number, should be 0xBE

    uint8_t myVariable;
	int ppm_minimumPulseTime[MAX_INPUT_CHANNELS]; // PPM min max calibration    
    int ppm_maximumPulseTime[MAX_INPUT_CHANNELS];
            
	float servo_center[MAX_OUTPUT_CHANNELS];
	float servo_calibrate_degree[MAX_OUTPUT_CHANNELS];
	float servo_calibrate_range[MAX_OUTPUT_CHANNELS];
	bool servo_reversed[MAX_OUTPUT_CHANNELS];
	
    uint8_t magic_ef;                       // magic number, should be 0xEF
    uint8_t chk;                            // XOR checksum    
} master_t;

extern master_t mcfg;   // profile config struct
