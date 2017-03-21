/* mbed R/C Servo Library
 * Copyright (c) 2007-2010 sford, cstyles
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
  
#ifndef MBED_SERVO_H
#define MBED_SERVO_H

//#include "mbed.h"
#include "PCA9685.h"


class Servo {

public:
    Servo(short servoNumber);
       
    void setPosition(float position);    
	float getPosition();
	void setPercent(float percent);
	void setController(PCA9685 *pwm);
    void calibrate(float range = 0.0005, float degrees = 45.0); 
    void setCenter(float pwm);
    float getCenter();
	void subTrim(float increment);
	void setReversed(bool reversed);
	void pwm(float width);
	void setServoPulse(double pulse);	
	float getDegrees();
	float getPwmUs();
	bool getReversed();
protected:
	float _position;
	float _center;
    PCA9685 *_pwm;
    float _pwmRange;
    float _positionRange;
    float _p;
	bool _reversed;
	short _servoNumber;
	float _pwmMs;
};

#endif
