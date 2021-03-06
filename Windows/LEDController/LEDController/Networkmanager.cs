﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Drawing;
using System.Diagnostics;

namespace LEDController
{
	class Networkmanager
	{
		private const int UDP_SOCKET = 6678;
		private const string BROADCAST_MESSAGE_ESP8266 = "ESP8266_BROADCAST";
		private const string BROADCAST_MESSAGE_REPLY = "HOST_REQUEST";

		private UdpClient _udpClient;

		private const int SEND_BUFFER_SIZE = 128;
		private byte[] _sendBuffer = new byte[SEND_BUFFER_SIZE];

		public Networkmanager()
		{
			_udpClient = new UdpClient(6678);
			_udpClient.Client.SendBufferSize = 64;

			ReadUDPData();
		}

		public void SendColorChangePackage(Color c)
		{
			InternalSendColorPackage(c);	
		}

		private async void InternalSendColorPackage(Color c)
		{
			if (_udpClient.Client.Connected)
			{
				int offset = 0;

				_sendBuffer[offset++] = (byte)Globals.NetMessageType.COLOR_CHANGE;
				_sendBuffer[offset++] = c.R;
				_sendBuffer[offset++] = c.G;
				_sendBuffer[offset++] = c.B;

				int ret = await _udpClient.SendAsync(_sendBuffer, offset);
			}
		}

		private async void ReadUDPData()
		{
			while (true)
			{

				UdpReceiveResult result = await _udpClient.ReceiveAsync();

				int offset = 0;
				Globals.NetMessageType messageTpye = (Globals.NetMessageType)result.Buffer[offset++];


				switch (messageTpye)
				{
					case Globals.NetMessageType.NONE:
						break;
					case Globals.NetMessageType.BROADCAST:

						//AS C# doesn only allow pointer like indexing of array in unsafe code, we have to actually create a copy of the massage....
						byte[] stringPart = new byte[result.Buffer.Length - offset];
						Array.Copy(result.Buffer, offset, stringPart, 0, stringPart.Length);

						string message = Encoding.Default.GetString(stringPart);
						if (message == BROADCAST_MESSAGE_ESP8266)
						{
							_udpClient.Connect(result.RemoteEndPoint.Address, result.RemoteEndPoint.Port);

							offset = 0;

							_sendBuffer[offset++] = (byte)Globals.NetMessageType.BROADCAST;

							byte[] reply = Encoding.Default.GetBytes(BROADCAST_MESSAGE_REPLY);

							Array.Copy(reply, 0, _sendBuffer, offset, reply.Length);
							offset += reply.Length;

							int ret = await _udpClient.SendAsync(_sendBuffer, offset);

							Debug.WriteLine("Sent response with result: " + ret.ToString());
						}
						break;
					case Globals.NetMessageType.COLOR_CHANGE:
						break;
				}


			}
		}
	}
}
