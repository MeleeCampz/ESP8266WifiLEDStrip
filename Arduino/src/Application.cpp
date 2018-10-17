#include "Application.h"

Application::Application()
{
}


Application::~Application()
{
}

void Application::Setup()
{
	Serial.begin(BAUD_RATE);
	//Wait for serial to init
	delay(1000);
	
	pinMode(PIN_R, OUTPUT);
	pinMode(PIN_G, OUTPUT);
	pinMode(PIN_B, OUTPUT);

	_networkManager.Begin();
}

void Application::Loop()
{
	long now = millis();
	float lerp = (now - _lastTime) / (float)TIME_TO_RISE;

	lerp = constrain(lerp, 0.0f, 1.0f);

	int writeVal = lerp * PWMRANGE;

	switch (_curColor)
	{
	case 0:
		analogWrite(PIN_R, lerp * writeVal);
		analogWrite(PIN_G, 0);
		analogWrite(PIN_B, 0);
		break;
	case 1:
		analogWrite(PIN_R, 0);
		analogWrite(PIN_G, lerp * writeVal);
		analogWrite(PIN_B, 0);
		break;
	case 2:
		analogWrite(PIN_R, 0);
		analogWrite(PIN_G, 0);
		analogWrite(PIN_B, lerp * writeVal);
		break;
	}


	if (lerp >= 1.0f)
	{
		_lastTime = millis();

		_curColor++;
		if (_curColor > 2)
		{
			_curColor = 0;
		}
	}

	_networkManager.Update();

	delay(10);
}