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

	analogWrite(PIN_R, 100);
	analogWrite(PIN_G, 100);
	analogWrite(PIN_B, 100);

	_networkManager.Begin();

	//Bind callback
	_networkManager.SetColorRequestCallback(std::bind(&Application::OnColorChangeRequested, this, std::placeholders::_1));
}

void Application::Loop()
{	
	_networkManager.Update();
}

void Application::OnColorChangeRequested(Color color)
{
	ChangeColor(color);
}

void Application::ChangeColor(Color color)
{
	analogWrite(PIN_R, color.r * TO_PWM);
	analogWrite(PIN_G, color.g * TO_PWM);
	analogWrite(PIN_B, color.b * TO_PWM);
}
