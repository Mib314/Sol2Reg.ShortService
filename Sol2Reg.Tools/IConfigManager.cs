// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\IConfigManager.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\IConfigManager.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\IConfigManager.cs
//     Created            : 21.01.2013 10:32
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools
{
	public interface IConfigManager
	{
		/// <summary>Reads the app settings on config file.</summary>
		/// <param name="key" >The key.</param>
		/// <returns>Value of this setting.</returns>
		string ReadAppSettings(string key);

		bool AppSettingKeyExist(string key);
	}
}