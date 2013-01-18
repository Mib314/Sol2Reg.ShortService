// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ServiceData\ModulesData.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ServiceData\ModulesData.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ServiceData\ModulesData.cs
//     Created            : 18.01.2013 22:18
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData.ServiceData
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	/// <summary>
	/// List of module data for WCF service.
	/// </summary>
	[DataContract]
	public class ModulesData
	{
		[DataMember]
		public IList<ModuleData> Modules { get; set; }
	}
}