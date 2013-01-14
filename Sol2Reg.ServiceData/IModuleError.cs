// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO.Interface\IModuleError.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO.Interface\IModuleError.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO.Interface\IModuleError.cs
//     Created            : 28.12.2012 16:42
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData
{
	using System;
	using System.Runtime.Serialization;
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