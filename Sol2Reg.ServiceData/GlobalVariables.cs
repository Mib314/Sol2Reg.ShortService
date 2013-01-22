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
	public class GlobalVariables : IGlobalVariables
	{
		public const string ModuleConfigName_Key = "ModuleConfigName";
		public const string ConfigFilePath_Key = "ConfigFilePath";
		public const double FloatPrecision = 0.0000001;
		public const int DefaultPort = 520;

		[Import]
		private IConfigManager configManager;

		private bool isInitialized;

		#region IGlobalVariables Members
		/// <summary>Gets the config file path.</summary>
		/// <value>The config file path.</value>
		public string ConfigFilePath { get; private set; }

		/// <summary>Gets the name of the module config.</summary>
		/// <value>The name of the module config.</value>
		public string ModuleConfigName { get; private set; }

		/// <summary>Initializes this instance.</summary>
		public void Initialize()
		{
			if (!this.isInitialized)
			{
				this.ModuleConfigName = this.configManager.ReadAppSettings(ModuleConfigName_Key);
				this.ConfigFilePath = this.configManager.ReadAppSettings(ConfigFilePath_Key);
				this.isInitialized = true;
			}
		}
		#endregion
	}
}