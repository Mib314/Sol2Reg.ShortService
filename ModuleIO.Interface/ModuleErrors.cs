// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO\ModuleErrors.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO\ModuleErrors.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO\ModuleErrors.cs
//     Created            : 29.12.2012 00:26
// </FileInfo>
//  ----------------------------------------------------------------------------------
namespace ModuleIO.Interface
{
	using System;
	using System.Collections.Generic;
	using ModuleIO.Interface.Enumerations;

	public class ModuleErrors:IModuleErrors

{
		/// <summary>Gets the current errors list.</summary>
		/// <value>The current errors list.</value>
		public IList<IModuleError> CurrentErrors { get; private set; }

		/// <summary>Gets the last error.</summary>
		/// <returns>Last error.</returns>
		public IModuleError GetLastError()
		{
			throw new NotImplementedException();
		}

		/// <summary>Clears the errors list.</summary>
		public void ClearErrors()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds the specified module_ name.
		/// </summary>
		/// <param name="module_Name">Name of the module_.</param>
		/// <param name="module_IpAddress">The module_ ip address.</param>
		/// <param name="module_Port">The module_ port.</param>
		/// <param name="errorCode">The error code.</param>
		/// <param name="errorTime">The error time.</param>
		public void Add(string module_Name, string module_IpAddress, int module_Port, ModuleErrorCode errorCode, DateTime errorTime)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds the specified module_ name.
		/// </summary>
		/// <param name="module_Name">Name of the module_.</param>
		/// <param name="module_IpAddress">The module_ ip address.</param>
		/// <param name="module_Port">The module_ port.</param>
		/// <param name="chanel_Id">The chanel_ id.</param>
		/// <param name="chanel_Key">The chanel_ key.</param>
		/// <param name="errorCode">The error code.</param>
		/// <param name="errorTime">The error time.</param>
		public void Add(string module_Name, string module_IpAddress, int module_Port, int chanel_Id, string chanel_Key, ModuleErrorCode errorCode, DateTime errorTime)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds the specified module_ name.
		/// </summary>
		/// <param name="module_Name">Name of the module_.</param>
		/// <param name="module_IpAddress">The module_ ip address.</param>
		/// <param name="module_Port">The module_ port.</param>
		/// <param name="chanel_Id">The chanel_ id.</param>
		/// <param name="chanel_Key">The chanel_ key.</param>
		/// <param name="chanel_Value">The chanel_ value.</param>
		/// <param name="errorCode">The error code.</param>
		/// <param name="errorTime">The error time.</param>
		public void Add(string module_Name, string module_IpAddress, int module_Port, int chanel_Id, string chanel_Key, string chanel_Value, ModuleErrorCode errorCode, DateTime errorTime)
		{
			throw new NotImplementedException();
		}
}
}