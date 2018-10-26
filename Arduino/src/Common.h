#pragma once
#include <Arduino.h>

struct Color
{
	short r;
	short g;
	short b;

	Color(int red, int green, int blue)
		:r(red), g(green), b (blue)
	{}

	Color(char* buffer, int* offset, int size)
	{
		if (size - *offset != 3)
		{
			SetColors(0, 0, 0);
			Serial.println("Invalid buffer length!");
		}
		
		SetColors
		(
			buffer[(*offset)++],
			buffer[(*offset)++],
			buffer[(*offset)++]
		);
	}

	void SetColors(short red, short green, short blue)
	{
		r = red;
		g = green;
		b = blue;
	}
};