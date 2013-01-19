// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ServiceData\ModulesData.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ServiceData\ModulesData.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ServiceData\ModulesData.cs
//     Created            : 31.12.2012 16:23
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class ModulesData
	{
		[DataMember]
		public IList<ModuleData> Modules { get; set; }
	}
}