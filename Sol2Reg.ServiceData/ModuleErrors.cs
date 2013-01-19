// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ServiceData\ModuleErrors.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ServiceData\ModuleErrors.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ServiceData\ModuleErrors.cs
//     Created            : 13.01.2013 22:37
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using System.Text;
	using Sol2Reg.ServiceData.Enumerations;

	[DataContract]
	public class ModuleErrors : IModuleErrors
	{
		#region IModuleErrors Members
		/// <summary>Gets the current errors list.</summary>
		/// <value>The current errors list.</value>
		[DataMember(Name = "Errors", IsRequired = true, Order = 0)]
		public IList<IModuleError> Errors { get; private set; }

		/// <summary>Gets the last error.</summary>
		/// <returns>Last error.</returns>
		public IModuleError GetLastError()
		{
			return this.Errors != null && this.Errors.Count > 0 ? this.Errors[this.Errors.Count - 1] : null;
		}

		/// <summary>Clears the errors list.</summary>
		public void ClearErrors()
		{
			this.Errors = null;
		}

		/// <summary>Adds the specified module_ name.</summary>
		/// <param name="module_Name" >Name of the module_.</param>
		/// <param name="module_IpAddress" >The module_ ip address.</param>
		/// <param name="module_Port" >The module_ port.</param>
		/// <param name="errorCode" >The error code.</param>
		/// <param name="errorTime" >The error time.</param>
		public void Add(string module_Name, string module_IpAddress, int module_Port, ModuleErrorCode errorCode, DateTime errorTime)
		{
			this.Errors.Add(new ModuleError {Module_Name = module_Name, Module_IpAddress = module_IpAddress, Module_Port = module_Port, ErrorCode = errorCode, ErrorTime = errorTime});
		}

		/// <summary>Adds the specified module_ name.</summary>
		/// <param name="module_Name" >Name of the module_.</param>
		/// <param name="module_IpAddress" >The module_ ip address.</param>
		/// <param name="module_Port" >The module_ port.</param>
		/// <param name="chanel_Id" >The chanel_ id.</param>
		/// <param name="chanel_Key" >The chanel_ key.</param>
		/// <param name="errorCode" >The error code.</param>
		/// <param name="errorTime" >The error time.</param>
		public void Add(string module_Name, string module_IpAddress, int module_Port, int chanel_Id, string chanel_Key, ModuleErrorCode errorCode, DateTime errorTime)
		{
			this.Errors.Add(new ModuleError {Module_Name = module_Name, Module_IpAddress = module_IpAddress, Module_Port = module_Port, Chanel_Id = chanel_Id, Chanel_Key = chanel_Key, ErrorCode = errorCode, ErrorTime = errorTime});
		}

		/// <summary>Adds the specified module_ name.</summary>
		/// <param name="module_Name" >Name of the module_.</param>
		/// <param name="module_IpAddress" >The module_ ip address.</param>
		/// <param name="module_Port" >The module_ port.</param>
		/// <param name="chanel_Id" >The chanel_ id.</param>
		/// <param name="chanel_Key" >The chanel_ key.</param>
		/// <param name="chanel_Value" >The chanel_ value.</param>
		/// <param name="errorCode" >The error code.</param>
		/// <param name="errorTime" >The error time.</param>
		public void Add(string module_Name, string module_IpAddress, int module_Port, int chanel_Id, string chanel_Key, string chanel_Value, ModuleErrorCode errorCode, DateTime errorTime)
		{
			this.Errors.Add(new ModuleError {Module_Name = module_Name, Module_IpAddress = module_IpAddress, Module_Port = module_Port, Chanel_Id = chanel_Id, Chanel_Key = chanel_Key, Chanel_Value = chanel_Value, ErrorCode = errorCode, ErrorTime = errorTime});
		}
		#endregion

		/// <summary>
		///     Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		///     A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			var errors = new StringBuilder(this.Errors.Count*202 + 1);

			foreach (var error in this.Errors)
			{
				errors.AppendLine(error.ToString());
			}

			return errors.ToString();
		}
	}
}