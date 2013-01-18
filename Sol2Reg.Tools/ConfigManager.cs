// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\Class1.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\Class1.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\Class1.cs
//     Created            : 16.01.2013 01:18
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools
{
	using System.ComponentModel.Composition;
	using System.Configuration;
	using System.Linq;

	[Export]
	public class ConfigManager
	{
		/// <summary>
		/// Reads the app settings on config file.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>Value of this setting.</returns>
		public string ReadAppSettings(string key)
		{
			return ConfigurationManager.AppSettings.AllKeys.FirstOrDefault(foo => foo == key);
		}
	}
}