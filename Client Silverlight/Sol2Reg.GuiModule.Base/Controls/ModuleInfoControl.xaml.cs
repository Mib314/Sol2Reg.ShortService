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

namespace Sol2Reg.GuiModule.Base.Controls
{
	using System.Windows.Controls;
	using Sol2Reg.GuiModule.Base.Model;

	/// <summary>Logique d'interaction pour Module_CheckConnection.xaml</summary>
	public partial class ModuleInfoControl : UserControl
	{
		private readonly ModuleInfoModel moduleInfo;
		public ModuleInfoControl()
		{
			this.InitializeComponent();

			this.moduleInfo = new ModuleInfoModel();

			this.DataContext = this.moduleInfo;
		}

		public ModuleInfoModel ModuleInfo { get; set; }
	}
}