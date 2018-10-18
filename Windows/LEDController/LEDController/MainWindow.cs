using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

using System.Net.Sockets;

namespace LEDController
{
	public partial class MainWindow : Form
	{
		private const int UDP_SOCKET = 6678;
		private const string BROADCAST_MESSAGE_ESP8266 = "ESP8266_BROADCAST";
		private const string BROADCAST_MESSAGE_REPLY = "HOST_REQUEST";
		private Button Btn_Red;
		private UdpClient _udpClient;

		public MainWindow()
		{
			InitializeComponent();

			_udpClient = new UdpClient(6678);

			ReadUDPData();
		}

		private async void ReadUDPData()
		{
			while(true)
			{
				UdpReceiveResult result = await _udpClient.ReceiveAsync();

				byte[] buffer = result.Buffer;
				string message = Encoding.Default.GetString(buffer);

				Debug.WriteLine(message);

				if(message == BROADCAST_MESSAGE_ESP8266)
				{
					_udpClient.Connect(result.RemoteEndPoint.Address, result.RemoteEndPoint.Port);


					byte[] sendBuffer = Encoding.Default.GetBytes(BROADCAST_MESSAGE_REPLY);

					int ret = await _udpClient.SendAsync(sendBuffer, sendBuffer.Length);

					Debug.WriteLine("Sent response with result: " + ret.ToString());
				}
			}
		}

		private void Btn_Red_Click(object sender, EventArgs e)
		{
			if(_udpClient.Client.Connected)
			{
				byte[] sendBuffer = Encoding.Default.GetBytes("255|000|000");
				_udpClient.Send(sendBuffer, sendBuffer.Length);
			}
		}

		private void Btn_Blue_Click(object sender, EventArgs e)
		{
			if (_udpClient.Client.Connected)
			{
				byte[] sendBuffer = Encoding.Default.GetBytes("000|255|000");
				_udpClient.Send(sendBuffer, sendBuffer.Length);
			}
		}

		private void Btn_Green_Click(object sender, EventArgs e)
		{
			if (_udpClient.Client.Connected)
			{
				byte[] sendBuffer = Encoding.Default.GetBytes("000|000|255");
				_udpClient.Send(sendBuffer, sendBuffer.Length);
			}
		}
	}
}
