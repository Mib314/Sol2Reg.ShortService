// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ShortService\GlobalVariables.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ShortService\GlobalVariables.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ShortService\GlobalVariables.cs
//     Created            : 28.12.2012 02:19
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

		public GlobalVariables()
		{
			this.ModuleConfigName = this.configManager.ReadAppSettings(ModuleConfigName_Key);
			this.ConfigFilePath = this.configManager.ReadAppSettings(ConfigFilePath_Key);
		}

		public  string ConfigFilePath { get; private set; }
		public string ModuleConfigName { get; private set; }
	}
}