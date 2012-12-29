// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO\ModuleErrors.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO\ModuleErrors.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO\ModuleErrors.cs
//     Created            : 29.12.2012 00:24
// </FileInfo>
//  ----------------------------------------------------------------------------------
namespace ModuleIO.Interface
{
	using System;
	using ModuleIO.Interface.Enumerations;

	public class ModuleError : IModuleError
	{
		public ModuleError(string Module_Name)
		{
			
		}
		/// <summary>
		/// Gets the name of the module.
		/// </summary>
		/// <value>
		/// The name of the module.
		/// </value>
		public string Module_Name { get; private set; }

		/// <summary>
		/// Gets the module ip address.
		/// </summary>
		/// <value>
		/// The module ip address.
		/// </value>
		public string Module_IpAddress { get; private set; }

		/// <summary>
		/// Gets the module port.
		/// </summary>
		/// <value>
		/// The module port.
		/// </value>
		public int Module_Port { get; private set; }

		/// <summary>
		/// Gets the chanel_ id.
		/// </summary>
		/// <value>
		/// The chanel_ id.
		/// </value>
		public int? Chanel_Id { get; private set; }

		/// <summary>
		/// Gets the chanel_ key.
		/// </summary>
		/// <value>
		/// The chanel_ key.
		/// </value>
		public string Chanel_Key { get; private set; }

		/// <summary>
		/// Gets the chanel_ value.
		/// </summary>
		/// <value>
		/// The chanel_ value.
		/// </value>
		public string Chanel_Value { get; private set; }

		/// <summary>
		/// Gets the error code of this error.
		/// </summary>
		/// <value>
		/// The error code.
		/// </value>
		public ModuleErrorCode ErrorCode { get; private set; }

		/// <summary>
		/// Gets the error time.
		/// </summary>
		/// <value>
		/// The error time.
		/// </value>
		public DateTime ErrorTime { get; private set; }
	}
}