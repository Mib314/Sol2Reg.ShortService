// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.ADAM6000Com\InitializerAdam6000.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.ADAM6000Com\InitializerAdam6000.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.ADAM6000Com\InitializerAdam6000.cs
//     Created            : 16.01.2013 14:21
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.ADAM6000Com
{
	using System;
	using System.ComponentModel.Composition;
	using Sol2Reg.IO.Interface;

	/// <summary>Initializer for Adam serie 6000.</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof (IInitializer))]
	public class InitializerAdam6000 : InitializerBase
	{
		/// <summary>Initialyses the module.</summary>
		public override IModuleBase InitializeModule(string moduleSerie, string moduleType, IModules modules)
		{
			if (moduleSerie != this.ModuleSerie_Key)
			{
				throw new ArgumentOutOfRangeException(moduleType);
			}
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
	}
}