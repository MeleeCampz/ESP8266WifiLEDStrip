#pragma once
#include<Arduino.h>

#include "NetworkManager.h"

class Application
{
public:
	Application();
	~Application();

	const int PIN_R = D8;
	const int PIN_G = D7;
	const int PIN_B = D6;

	const int BAUD_RATE = 921600;

	void Setup();
	void Loop();

private:
	const long TIME_TO_RISE = 2000;
	long _lastTime = 0;
	int _curColor = 0;

	NetworkManager _networkManager;
};

