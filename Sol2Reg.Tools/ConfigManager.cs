// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\ConfigManager.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\ConfigManager.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\ConfigManager.cs
//     Created            : 16.01.2013 01:18
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools
{
	using System.ComponentModel.Composition;
	using System.Configuration;
	using System.Linq;

	/// <summary>Tools for configuration (read and write).</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class ConfigManager : IConfigManager
	{
		#region AppSettings
		/// <summary>Reads the app settings on config file.</summary>
		/// <param name="key" >The key.</param>
		/// <returns>Value of this setting.</returns>
		public string ReadAppSettings(string key)
		{
			return this.AppSettingKeyExist(key) ? ConfigurationManager.AppSettings[key] : string.Empty;
		}

		public bool AppSettingKeyExist(string key)
		{
			return ConfigurationManager.AppSettings.AllKeys.Count(foo => foo == key) > 0;
		}
		#endregion
	}
}