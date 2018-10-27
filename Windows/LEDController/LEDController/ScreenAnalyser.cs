using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Threading;
using System.Diagnostics;

namespace LEDController
{
	class ScreenAnalyser
	{
		//TODO: Make this threadsage!
		public Color CurrentAverage { get; set; }

		private Bitmap _screenShot;
		private Bitmap _scaledScreenShot;

		private const int downScale = 16;

		private const int _screenWidth = 1920 / downScale;
		private const int _screenHeight = 1080 / downScale;

		private const float marginPercent = 0.125f;
		private const int minX = (int)(_screenWidth * marginPercent);
		private const int minY = (int)(_screenHeight * marginPercent);
		private const int maxX = _screenWidth - minX;
		private const int maxY = _screenHeight - minY;

		private Thread _worker;
		private Networkmanager _networkmanager;

		private Size currentScreen = new Size((int)System.Windows.SystemParameters.PrimaryScreenWidth, (int)System.Windows.SystemParameters.PrimaryScreenHeight);

		public ScreenAnalyser(Networkmanager networkmanager)
		{
			_networkmanager = networkmanager;

			_screenShot = new Bitmap(currentScreen.Width, currentScreen.Height);
			CurrentAverage = Color.Pink;
		}

		public void Run()
		{
			_worker = new Thread(UpdateTimer);
			_worker.Start();
		}

		private void UpdateTimer()
		{
			int counter = 0;
			long result = 0;
			Stopwatch watch = new Stopwatch();

			//TODO: Add exit condintion
			while (true)
			{
				watch.Start();
				//
				Tick();
				//
				watch.Stop();
				result += watch.ElapsedTicks;
				watch.Reset();

				counter++;

				if (counter >= 100)
				{
					float avg = result / counter;

					Debug.Print((avg / Stopwatch.Frequency * 1000).ToString());

					counter = 0;
					result = 0;
				}

				//Thread.Sleep(5);
			}
		}

		private void Tick()
		{
			GetScreenShot();
			_networkmanager.SendColorChangePackage(CurrentAverage);
		}

		private void GetScreenShot()
		{
			try
			{
				Graphics gfxScreenshot = Graphics.FromImage(_screenShot); //1
				gfxScreenshot.CopyFromScreen(0, 0, 0, 0, currentScreen);
				_scaledScreenShot = new Bitmap(_screenShot, _screenWidth, _screenHeight);
				gfxScreenshot.Clear(Color.Empty);
				//Resize(scaledBmpScreenshot).Save("6regular.bmp");
			}
			catch
			{
				_scaledScreenShot = new Bitmap(1, 1);
				_scaledScreenShot.SetPixel(0, 0, Color.Black);
			}

			long r = 0;
			long g = 0;
			long b = 0;

			int pixelSum = _scaledScreenShot.Width * _scaledScreenShot.Height;

			Color current;

			for (int y = minY; y < maxY; y++)
			{
				for (int x = minX; x < maxX; x++)
				{
					current = _scaledScreenShot.GetPixel(x, y);
					r += current.R;
					g += current.G;
					b += current.B;
				}
			}

			r /= pixelSum;
			g /= pixelSum;
			b /= pixelSum;

			CurrentAverage = Color.FromArgb((int)r, (int)g, (int)b);
		}
	}
}
