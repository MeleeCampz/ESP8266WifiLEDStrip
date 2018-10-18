#pragma once
#include <Arduino.h>

struct Color
{
	int r;
	int g;
	int b;

	Color(int red, int green, int blue)
		:r(red), g(green), b (blue)
	{}

	Color(String s)
	{
		if (s.length() != 11)
		{
			SetColors(0, 0, 0);
			Serial.println("Invalid colors string!");
		}

		String red = s.substring(0, 2);
		String green = s.substring(4, 6);
		String blue = s.substring(8, 10);

		SetColors(atoi(red.c_str()), atoi(green.c_str()), atoi(blue.c_str()));
	}

	void SetColors(int red, int green, int blue)
	{
		r = red;
		g = green;
		b = blue;
	}
};