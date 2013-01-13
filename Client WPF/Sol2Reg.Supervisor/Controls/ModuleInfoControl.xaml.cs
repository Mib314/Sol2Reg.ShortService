// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Supervisor\Module_CheckConnection.xaml.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Supervisor\Module_CheckConnection.xaml.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Supervisor\Module_CheckConnection.xaml.cs
//     Created            : 05.01.2013 20:40
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Supervisor.Controls
{
	using System.Windows.Controls;
	using Sol2Reg.Supervisor.Model;

	/// <summary>Logique d'interaction pour Module_CheckConnection.xaml</summary>
	public partial class ModuleInfoControl : UserControl
	{
		private readonly ModuleInfoModel moduleInfo;
		public ModuleInfoControl()
		{
			this.InitializeComponent();

			moduleInfo = new ModuleInfoModel();

			DataContext = moduleInfo;
		}

		public ModuleInfoModel ModuleInfo { get; set; }
	}
}