// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Superviseur\MainWindow.xaml.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Superviseur\MainWindow.xaml.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Superviseur\MainWindow.xaml.cs
//     Created            : 18.12.2012 01:30
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Superviseur
{
	using System.Globalization;
	using System.Linq;
	using System.Windows;
	using ADAM6000Com;
	using ModuleIO;
	using ModuleIO.Interface;

	/// <summary>Logique d'interaction pour MainWindow.xaml</summary>
	public partial class MainWindow
	{
		private readonly IModuleBase adam6015;
		private readonly IModuleBase adam6066;
		private readonly IModules modules;

		public MainWindow()
		{
			this.InitializeComponent();
			var initializeModules = new InitializeModules();
			this.modules = initializeModules.Modules;
			this.adam6015 = this.modules.FirstOrDefault(foo => foo.ModuleSerie == "Adam6015");
			this.adam6066 = this.modules.FirstOrDefault(foo => foo.ModuleSerie == "Adam6066");

			this.Ip6015.Text = this.adam6015.IpAddress;
			this.Ip6066.Text = this.adam6066.IpAddress;
		}

		private void _66ch0_OnClick(object sender, RoutedEventArgs e)
		{
			this.adam6066.WriteData(6, this._66ch0.IsChecked);
			this._66ch0.IsChecked = this.adam6066.Chanels[6].ValueDigital;
		}

		private void connect_OnClick(object sender, RoutedEventArgs e)
		{
			this.adam6015.Start();
			this.adam6066.Start();
			try
			{
				this.chanelInfo15.Text = string.Format("Start in ch {0}. Total in ch {1}", ((Adam60Xx)this.adam6015).IdSartForInputChanel, ((Adam60Xx)this.adam6015).TotalChanelAnalaogIn);
			}
			catch
			{
				this.chanelInfo15.Text = "Error to read data.";
			}
			try
			{
				this.chanelInfo66.Text = string.Format("Start in ch {0}. Total in ch {1}. Start out ch {2}. Total out ch {3}", ((Adam60Xx)this.adam6066).IdSartForInputChanel, ((Adam60Xx)this.adam6066).TotalChanelDigitalIn, ((Adam60Xx)this.adam6066).IdSartForOutputChanel, ((Adam60Xx)this.adam6066).TotalChanelDigitalOut);
			}
			catch
			{
				this.chanelInfo66.Text = "Error to read data.";
			}
			this.WriteError();
		}

		private
		void readData_OnClick
		(object sender, RoutedEventArgs e)
		{
			this.adam6015.ReadData();
			this.adam6066.ReadData();
			this._15ch0.Text = (this.adam6015.Chanels[0].ValueAnalog ?? -1000).ToString(CultureInfo.InvariantCulture) + " °";
			this._66ch0.IsChecked = this.adam6066.Chanels[6].ValueDigital;
			this.WriteError();
		}

		private
			void WriteError
			()
		{
			this.lastError15.Text = "Module 6015 : " + this.adam6015.Errors.ToString();
			this.lastError66.Text = "Module 6066 : " + this.adam6066.Errors.ToString();
		}
	}
}