using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEDController
{
	class Globals
	{
		public enum NetMessageType : byte
		{
			NONE = 0,
			BROADCAST = 1,
			COLOR_CHANGE = 2
		}
	}
}
