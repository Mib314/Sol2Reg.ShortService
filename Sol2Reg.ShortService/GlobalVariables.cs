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

namespace Sol2Reg.ShortService
{
	using System.Configuration;

	public static class GlobalVariables
	{
		public const string ModuleConfigName_Key = "ModuleConfigName";
		public const string ConfigFilePath_Key = "ConfigFilePath";

		public static readonly string ModuleConfigName;
		public static readonly string ConfigFilePath;

		static GlobalVariables()
		{
			ModuleConfigName = ConfigurationManager.AppSettings[ModuleConfigName_Key];
			ConfigFilePath = ConfigurationManager.AppSettings[ConfigFilePath_Key];
		}
	}
}