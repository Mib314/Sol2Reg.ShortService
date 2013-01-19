// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Interface\IInitializer.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Interface\IInitializer.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Interface\IInitializer.cs
//     Created            : 16.01.2013 15:26
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Interface
{
	using System.ComponentModel.Composition;

	/// <summary>
	/// Initializer one type of moduleIO.
	/// </summary>
	[InheritedExport]
	public interface IInitializer
	{
		/// <summary>Initialyses the module.</summary>
		IModuleBase InitializeModule(string moduleSerie, string moduleType, IModules modules);

		/// <summary>Gets the module serie key.</summary>
		/// <value>The module serie key.</value>
		string ModuleSerie_Key { get; }
	}
}