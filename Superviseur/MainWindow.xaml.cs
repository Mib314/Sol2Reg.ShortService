using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ADAM6000Com;

namespace Superviseur
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Adam6015 adam6015;
		private Adam6066 adam6066;
		public MainWindow()
		{
			InitializeComponent();

			this.adam6015 = new Adam6015();
			this.adam6066 = new Adam6066();
		}

		private void _66ch0_OnClick(object sender, RoutedEventArgs e)
		{
			this.adam6066.WriteData(6, _66ch0.IsChecked);
			this._66ch0.IsChecked = this.adam6066.Chanels[6].ValueDigital;
		}

		private void connect_OnClick(object sender, RoutedEventArgs e)
		{
			this.adam6015.IpAddress = Ip6015.Text;
			this.adam6066.IpAddress = Ip6066.Text;

			this.adam6015.Start();
			this.adam6066.Start();

			this.chanelInfo15.Text = string.Format("Start in ch {0}. Total in ch {1}", this.adam6015.IdSartForInputChanel, this.adam6015.TotalChanelAnalaogIn);
			this.chanelInfo66.Text = string.Format("Start in ch {0}. Total in ch {1}. Start out ch {2}. Total out ch {3}", this.adam6066.IdSartForInputChanel, this.adam6066.TotalChanelDigitalIn, this.adam6066.IdSartForOutputChanel, this.adam6066.TotalChanelDigitalOut);
			WriteError();
		}

		private void readData_OnClick(object sender, RoutedEventArgs e)
		{
			this.adam6015.ReadData();
			this.adam6066.ReadData();
			this._15ch0.Text = (this.adam6015.Chanels[0].ValueAnalog ?? -1000).ToString(CultureInfo.InvariantCulture) + " °";
			this._66ch0.IsChecked = this.adam6066.Chanels[6].ValueDigital;
			WriteError();
		}

		private void WriteError()
		{
			this.lastError15.Text = "Module 6015 : " + this.adam6015.LastError.ToString();
			this.lastError66.Text = "Module 6066 : " + this.adam6066.LastError.ToString();
		}
	}
}
