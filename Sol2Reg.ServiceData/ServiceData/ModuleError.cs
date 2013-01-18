// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO.Interface\ModuleError.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO.Interface\ModuleError.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO.Interface\ModuleError.cs
//     Created            : 29.12.2012 00:28
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData.ServiceData
{
	using System;
	using System.Runtime.Serialization;
	using Sol2Reg.ServiceData.Enumerations;

	/// <summary>
	/// Module error for WCF service
	/// </summary>
	[DataContract]
	public class ModuleError : IModuleError
	{
		#region IModuleError Members
		/// <summary>Gets the name of the module.</summary>
		/// <value>The name of the module.</value>
		[DataMember(Name = "MN", IsRequired = true, Order = 0)]
		public string Module_Name { get; set; }

		/// <summary>Gets the module ip address.</summary>
		/// <value>The module ip address.</value>
		[DataMember(Name = "IP", IsRequired = true, Order = 1)]
		public string Module_IpAddress { get; set; }

		/// <summary>Gets the module port.</summary>
		/// <value>The module port.</value>
		[DataMember(Name = "PO", IsRequired = false, Order = 2)]
		public int Module_Port { get; set; }

		/// <summary>Gets the chanel_ id.</summary>
		/// <value>The chanel_ id.</value>
		[DataMember(Name = "CID", IsRequired = false, Order = 10)]
		public int? Chanel_Id { get; set; }

		/// <summary>Gets the chanel_ key.</summary>
		/// <value>The chanel_ key.</value>
		[DataMember(Name = "CKE", IsRequired = false, Order = 11)]
		public string Chanel_Key { get; set; }

		/// <summary>Gets the chanel_ value.</summary>
		/// <value>The chanel_ value.</value>
		[DataMember(Name = "CVA", IsRequired = false, Order = 12)]
		public string Chanel_Value { get; set; }

		/// <summary>Gets the error code of this error.</summary>
		/// <value>The error code.</value>
		public ModuleErrorCode ErrorCode { get; set; }

		/// <summary>Gets the error time.</summary>
		/// <value>The error time.</value>
		[DataMember(Name = "ET", IsRequired = true, Order = 20)]
		public DateTime ErrorTime { get; set; }
		#endregion

		public override string ToString()
		{
			return string.Format("Mod. {0}, Ch Id {1}, Ch key {2}, value {3}.\n" , this.Module_Name, this.Chanel_Id, this.Chanel_Key, this.Chanel_Value);
		}
	}
}