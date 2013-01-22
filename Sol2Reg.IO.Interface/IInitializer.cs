// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Interface\IInitializer.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Interface\IInitializer.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Interface\IInitializer.cs
//     Created            : 16.01.2013 14:26
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Interface
{
	/// <summary>Initializer one type of moduleIO.</summary>
	public interface IInitializer
	{
		/// <summary>Gets the module serie key.</summary>
		/// <value>The module serie key.</value>
		string ModuleSerie_Key { get; }

		/// <summary>Initialyses the module.</summary>
		IModuleBase InitializeModule(string moduleSerie, string moduleType, IModules modules);
	}
}