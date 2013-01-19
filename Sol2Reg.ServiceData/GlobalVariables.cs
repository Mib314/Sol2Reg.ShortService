// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ServiceData\GlobalVariables.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ServiceData\GlobalVariables.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ServiceData\GlobalVariables.cs
//     Created            : 16.01.2013 09:06
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData
{
	using System.ComponentModel.Composition;
	using Sol2Reg.Tools;

	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class GlobalVariables
	{
		public const string ModuleConfigName_Key = "ModuleConfigName";
		public const string ConfigFilePath_Key = "ConfigFilePath";

		[Import]
		private ConfigManager configManager;

		private bool isInitialized;

		public string ConfigFilePath { get; private set; }
		public string ModuleConfigName { get; private set; }

		public void Initialize()
		{
			if (!this.isInitialized)
			{
				this.ModuleConfigName = this.configManager.ReadAppSettings(ModuleConfigName_Key);
				this.ConfigFilePath = this.configManager.ReadAppSettings(ConfigFilePath_Key);
				this.isInitialized = true;
			}
		}
	}
}