// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ServiceData\ModuleData.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ServiceData\ModuleData.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ServiceData\ModuleData.cs
//     Created            : 31.12.2012 16:20
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData.ServiceData
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	/// <summary>
	/// Module data for WCF service.
	/// </summary>
	[DataContract]
	public class ModuleData
	{
		#region Properties
		/// <summary>Gets or sets the name.</summary>
		/// <value>The name.</value>
		[DataMember(Name = "N", IsRequired = true, Order = 0)]
		public string Name { get; set; }

		/// <summary>Gets the module serie.</summary>
		/// <value>The module serie.</value>
		[DataMember(Name = "MS", IsRequired = true, Order = 1)]
		public string ModuleSerie { get; set; }

		/// <summary>Gets the type of the module.</summary>
		/// <value>The type of the module.</value>
		[DataMember(Name = "MT", IsRequired = true, Order = 2)]
		public string ModuleType { get; set; }

		/// <summary>Gets or sets the ip address.</summary>
		/// <value>The ip address.</value>
		[DataMember(Name = "IP", IsRequired = true, Order = 10)]
		public string IpAddress { get; set; }

		/// <summary>Gets or sets the port.</summary>
		/// <value>The port.</value>
		[DataMember(Name = "P", IsRequired = false, Order = 11)]
		public int Port { get; set; }

		/// <summary>Gets the chanels.</summary>
		/// <value>The chanels. Definition and current value.</value>
		[DataMember(Name = "CH", IsRequired = false, Order = 20)]
		public IList<ChanelData> Chanels { get; set; }

		/// <summary>Gets the errors list.</summary>
		/// <value>The errors.</value>
		[DataMember(Name = "ER", IsRequired = false, Order = 100)]
		public IModuleErrors Errors { get; set; }
		#endregion
	}
}