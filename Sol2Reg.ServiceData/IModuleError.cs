// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ServiceData\IModuleError.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ServiceData\IModuleError.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ServiceData\IModuleError.cs
//     Created            : 13.01.2013 22:26
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData
{
	using System;
	using Sol2Reg.ServiceData.Enumerations;

	public interface IModuleError
	{
		/// <summary>Gets the name of the module.</summary>
		/// <value>The name of the module.</value>
		string Module_Name { get; }

		/// <summary>Gets the module ip address.</summary>
		/// <value>The module ip address.</value>
		string Module_IpAddress { get; }

		/// <summary>Gets the module port.</summary>
		/// <value>The module port.</value>
		int Module_Port { get; }

		/// <summary>Gets the chanel_ id.</summary>
		/// <value>The chanel_ id.</value>
		int? Chanel_Id { get; }

		/// <summary>Gets the chanel_ key.</summary>
		/// <value>The chanel_ key.</value>
		string Chanel_Key { get; }

		/// <summary>Gets the chanel_ value.</summary>
		/// <value>The chanel_ value.</value>
		string Chanel_Value { get; }

		/// <summary>Gets the error code of this error.</summary>
		/// <value>The error code.</value>
		ModuleErrorCode ErrorCode { get; }

		/// <summary>Gets the error time.</summary>
		/// <value>The error time.</value>
		DateTime ErrorTime { get; }
	}
}