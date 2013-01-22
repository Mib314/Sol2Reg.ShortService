// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ServiceData\IModuleErrors.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ServiceData\IModuleErrors.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ServiceData\IModuleErrors.cs
//     Created            : 13.01.2013 22:26
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData
{
	using System;
	using System.Collections.Generic;
	using Sol2Reg.ServiceData.Enumerations;

	public interface IModuleErrors
	{
		/// <summary>Gets the current errors list.</summary>
		/// <value>The current errors list.</value>
		IList<IModuleError> Errors { get; }

		/// <summary>Gets the last error.</summary>
		/// <returns>Last error.</returns>
		IModuleError GetLastError();

		/// <summary>Clears the errors list.</summary>
		void ClearErrors();

		/// <summary>Adds the specified module_ name.</summary>
		/// <param name="module_Name" >Name of the module_.</param>
		/// <param name="module_IpAddress" >The module_ ip address.</param>
		/// <param name="module_Port" >The module_ port.</param>
		/// <param name="errorCode" >The error code.</param>
		/// <param name="errorTime" >The error time.</param>
		void Add(string module_Name, string module_IpAddress, int module_Port, ModuleErrorCode errorCode, DateTime errorTime);

		/// <summary>Adds the specified module_ name.</summary>
		/// <param name="module_Name" >Name of the module_.</param>
		/// <param name="module_IpAddress" >The module_ ip address.</param>
		/// <param name="module_Port" >The module_ port.</param>
		/// <param name="chanel_Id" >The chanel_ id.</param>
		/// <param name="chanel_Key" >The chanel_ key.</param>
		/// <param name="errorCode" >The error code.</param>
		/// <param name="errorTime" >The error time.</param>
		void Add(string module_Name, string module_IpAddress, int module_Port, int chanel_Id, string chanel_Key, ModuleErrorCode errorCode, DateTime errorTime);

		/// <summary>Adds the specified module_ name.</summary>
		/// <param name="module_Name" >Name of the module_.</param>
		/// <param name="module_IpAddress" >The module_ ip address.</param>
		/// <param name="module_Port" >The module_ port.</param>
		/// <param name="chanel_Id" >The chanel_ id.</param>
		/// <param name="chanel_Key" >The chanel_ key.</param>
		/// <param name="chanel_Value" >The chanel_ value.</param>
		/// <param name="errorCode" >The error code.</param>
		/// <param name="errorTime" >The error time.</param>
		void Add(string module_Name, string module_IpAddress, int module_Port, int chanel_Id, string chanel_Key, string chanel_Value, ModuleErrorCode errorCode, DateTime errorTime);
	}
}