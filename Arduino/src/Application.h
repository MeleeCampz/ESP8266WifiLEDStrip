#pragma once
#include "Common.h"

#include <Arduino.h>
#include "NetworkManager.h" 

class Application
{
public:
	Application();
	~Application();

	const int PIN_R = D8;
	const int PIN_G = D7;
	const int PIN_B = D6;

	const float TO_PWM = PWMRANGE / 256;

	const int BAUD_RATE = 921600;

	void Setup();
	void Loop();

private:

	NetworkManager _networkManager;

	void OnColorChangeRequested(Color color);
	void ChangeColor(Color color);
};

