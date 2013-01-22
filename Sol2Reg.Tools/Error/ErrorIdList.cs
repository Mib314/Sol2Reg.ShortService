// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\ErrorIdList.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\ErrorIdList.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\ErrorIdList.cs
//     Created            : 21.01.2013 19:27
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools.Error
{
	/// <summary>List off error.</summary>
	public enum ErrorIdList
	{
		/// <summary>The exception</summary>
		Exception = 0,

		#region ConfigModuleIO
		/// <summary>The config module Io => No file</summary>
		ConfigModuleIO_NoFile = 100,

		/// <summary>The config module I o_ file bad formated</summary>
		/// <remarks>Parameter : 1 (Config file name)</remarks>
		ConfigModuleIO_FileBadFormated = 101,

		/// <summary>The config module Io => No tag modules</summary>
		/// <remarks>Parameter : 1 (Config file name)</remarks>
		ConfigModuleIO_NoTagModules = 110,

		/// <summary>The config module Io => No tag module</summary>
		/// <remarks>Parameter : 1 (Config file name)</remarks>
		ConfigModuleIO_NoTagModule = 111,
		/// <summary>
		/// The config module Io => module data not valid
		/// </summary>
		/// <remarks>Parameter : 1 (Module name)</remarks>
		ConfigModuleIO_ModuleDataNotValid = 120,

		/// <summary>The config module Io read chanel</summary>
		/// <remarks>Parameter : 2 (Config file name, module name)</remarks>
		ConfigModuleIO_ReadChanel = 130,
		#endregion

	}
}