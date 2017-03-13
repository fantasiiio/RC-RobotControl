/* mbed R/C Servo Library
 *  
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
  
// Modified by Francois Girard, 2017

#include "Servo.h"


static float clamp(float value, float min, float max) {
    if(value < min) {
        return min;
    } else if(value > max) {
        return max;
    } else {
        return value;
    }
}

Servo::Servo(short servoNumber) {
	_servoNumber = servoNumber;
    calibrate();
    setPosition(0);
}

// you can use this function if you'd like to set the pulse length in seconds
// e.g. setServoPulse(0, 0.001) is a ~1 millisecond pulse width. its not precise!
void Servo::setServoPulse(double pulse) {
  double pulselength, pwm;
  if(_pwm == NULL)
	  return;

  _pwmMs = pulse; // Informative
  pulselength = 1;   // 1 second
  pulselength /= _pwm->getPWMFreq(); // 60 Hz
  //pulselength /= 4096;  // 12 bits of resolution
  pwm = pulse * 4096 / pulselength;
  _pwm->setPWM(_servoNumber, 0, pwm);
}

void Servo::setPercent(float percent)
{
	float angle = _positionRange * 2 * percent - _positionRange;
	setPosition(angle);
}

void Servo::setPosition(float position) {
	float offset;	
	_position = clamp(position, -_positionRange, _positionRange);
	offset = _pwmRange * (_position / _positionRange);
	if(_reversed)
		offset = -offset;
	setServoPulse(_center + clamp(offset, -_pwmRange, _pwmRange));
}

// range in microseconds
// degrees of range
void Servo::calibrate(float range, float degrees) {
    _pwmRange = range;
    _positionRange = degrees;
	setPosition(_position);
}

float Servo::getDegrees()
{
	return _positionRange;
}

void Servo::pwm(float width)
{
	width /= 1000000.0;
	setServoPulse((float)width);
	_position = _positionRange * ((width - _center) / _pwmRange);
}

void Servo::setCenter(float center) {
	_center = center;
	setPosition(_position);
}

float Servo::getCenter()
{
	return _center;
}

void Servo::subTrim(float increment) {
	_center += increment;
	setPosition(_position);
}

void Servo::setReversed(bool reversed)
{
	_reversed = reversed;
	setPosition(_position);
}

void Servo::setController(PCA9685 *pwm)
{
	_pwm = pwm;
}

float Servo::getPosition()
{
	return _position;
}

float Servo::getPwmUs()
{
	return _pwmMs * 1000000;
}
