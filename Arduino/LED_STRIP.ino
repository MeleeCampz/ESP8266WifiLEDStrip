/*
    Name:       LED_STRIP.ino
    Created:	16-Oct-18 9:27:05 PM
    Author:     MELEECAMPZ\Tobias
*/

#include "Arduino.h"

int rPin = D8;
int gPin = D7; 
int bPin = D6;

void setup()
{
	pinMode(rPin, OUTPUT);
	pinMode(gPin, OUTPUT);
	pinMode(bPin, OUTPUT);

	Serial.begin(921600);
}

long lastTime = 0;
long timeToRise = 2000;

int curColor = 0;

void loop()
{
	long now = millis();
	float lerp = (now - lastTime) / (float)timeToRise;

	lerp = constrain(lerp, 0.0f, 1.0f);

	int writeVal = lerp * PWMRANGE;

	switch (curColor)
	{
		case 0:
			analogWrite(rPin, lerp * writeVal);
			analogWrite(gPin, 0);
			analogWrite(bPin, 0);
			break;
		case 1:
			analogWrite(rPin, 0);
			analogWrite(gPin, lerp * writeVal);
			analogWrite(bPin, 0);
			break;
		case 2:
			analogWrite(rPin, 0);
			analogWrite(gPin, 0);
			analogWrite(bPin, lerp * writeVal);
			break;
	}
	


	if (lerp >= 1.0f)
	{
		lastTime = millis();

		curColor++;
		if (curColor > 2)
		{
			curColor = 0;
		}
	}

	delay(10);
}
 