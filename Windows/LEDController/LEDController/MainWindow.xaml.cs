using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Diagnostics;
using System.Net.Sockets;
using System.Drawing;

namespace LEDController
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ScreenAnalyser _screenAnalyser;
		private Networkmanager _networkmanager;

		public MainWindow()
		{
			InitializeComponent();

			_networkmanager = new Networkmanager();

			_screenAnalyser = new ScreenAnalyser(_networkmanager);
			_screenAnalyser.Run();
		}

		private void Btn_red_Click(object sender, RoutedEventArgs e)
		{
			_networkmanager.SendColorChangePackage(Color.Red);
		}

		private void Btn_grren_Click(object sender, RoutedEventArgs e)
		{
			_networkmanager.SendColorChangePackage(Color.Green);
		}

		private void Btn_blue_Click(object sender, RoutedEventArgs e)
		{
			_networkmanager.SendColorChangePackage(Color.Blue);
		}
	}
}
