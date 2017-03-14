/**Library for PCA9685 16-channel, 12-bit PWM Fm+ I²C-bus LED controller
* Example code
* @code
*
*#include"PCA9685.h"
*#include"mbed.h"
*
*
*
*PCA9685 pwm(D14,D15);
* 
*void setServoPulse(uint8_t n, float pulse) {
*    float pulselength = 10000;   // 10,000 units per seconds
*    pulse = 4094 * pulse / pulselength;
*    pwm.setPWM(n, 0, pulse);
*}
* 
*void initServoDriver() {
*    pwm.begin();
*    pwm.setPrescale(64);    //This value is decided for 10ms interval.
*    pwm.frequencyI2C(400000); //400kHz
*}
* 
* int main() {
*
*    while(1){
*    initServoDriver();
*    wait(0.2);
*    setServoPulse(0, 2300);
*    setServoPulse(1, 500);    
*    wait(0.5);//delay necessary to perform the action
*    setServoPulse(0, 1350);
*    setServoPulse(1, 1350);
*    wait(0.5);
*    setServoPulse(0,550);
*    setServoPulse(1, 2250);
*    wait(0.5);
*    setServoPulse(0, 2300);
*    wait(2);
*    for (int mov = 550; mov < 2300; mov++){
*    setServoPulse(0, mov);
*    wait(0.001); 
*    }  
*    for (int mov = 500; mov < 2200; mov++){
*    setServoPulse(1, mov);
*    wait(0.001); 
*    }     
*   }
*}
*@endcode
*
*/
#ifndef PCA9685_H
#define PCA9685_H

#include "mbed.h"
#include <cmath>
//register definitions
#define PCA9685_SUBADR1 0x2
#define PCA9685_SUBADR2 0x3
#define PCA9685_SUBADR3 0x4

#define PCA9685_MODE1 0x0
#define PCA9685_PRESCALE 0xFE

#define LED0_ON_L 0x6
#define LED0_ON_H 0x7
#define LED0_OFF_L 0x8
#define LED0_OFF_H 0x9

#define ALLLED_ON_L 0xFA
#define ALLLED_ON_H 0xFB
#define ALLLED_OFF_L 0xFC
#define ALLLED_OFF_H 0xFD


class PCA9685
{
public:
    PCA9685(PinName sda, PinName scl, int addr = 0x80);
    void frequencyI2C(int freq);
    void begin(void); //Initialize the controller
    void reset(void); //Reset the controller
    void setPrescale(uint8_t prescale);//setPrescale(prescale)
    void setPWMFreq(float freq);//Set the pwm frequency
    float getPWMFreq();//get the pwm frequency
    void setPWM(uint8_t num, uint16_t on, uint16_t off);//SetPWM(channel, on, off)
    /** Set the start (on) and the end (off) of the part of the PWM pulse of the channel
     *  @param channel : from 0 to 15 the channel the should be update
     *  @param  on: from 0 to 4095 the tick when the signal should pass from low to high
     *  @param off: from 0 to 4095 the tick when the signal should pass from high to low
     */
private:
    void write8(uint8_t address, uint8_t data);
    char read8(char address);
    int _i2caddr;
    I2C i2c;
	float _freq;
};

#endif

