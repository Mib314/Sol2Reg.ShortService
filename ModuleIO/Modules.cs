// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO\Modules.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO\Modules.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO\Modules.cs
//     Created            : 28.12.2012 04:43
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ModuleIO
{
	using System.Collections.Generic;
	using ModuleIO.Interface;

	/// <summary>List of module.</summary>
	public class Modules : List<IModuleBase>, IModules {}
}