// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.ADAM6000Com\Initialzer.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.ADAM6000Com\Initialzer.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.ADAM6000Com\Initialzer.cs
//     Created            : 16.01.2013 15:21
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.ADAM6000Com
{
	using System;
	using System.ComponentModel.Composition;
	using Sol2Reg.IO.Interface;

	/// <summary>
	/// Initializer for Adam serie 6000.
	/// </summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(IInitializer))]
	public class InitializerAdam6000 : IInitializer
	{
		// List of Adam serie
		private const string MODULE_SERIE_ADAM6000 = "Adam6000Type";

		/// <summary>Gets the module serie key.</summary>
		/// <value>The module serie key.</value>
		public string ModuleSerie_Key
		{
			get { return MODULE_SERIE_ADAM6000; }
		}

		#region IInitializer Members
		/// <summary>Initialyses the module.</summary>
		public IModuleBase InitializeModule(string moduleSerie, string moduleType, IModules modules)
		{
			if (moduleSerie != MODULE_SERIE_ADAM6000) throw new ArgumentOutOfRangeException(moduleType);
			switch (moduleType)
			{
				case "Adam6015":
					return new Adam6015().Initialize(moduleSerie, moduleType, modules);
				case "Adam6066":
					return new Adam6066().Initialize(moduleSerie, moduleType, modules);
				default:
					throw new ArgumentOutOfRangeException(moduleType);
			}
		}
		#endregion
	}
}