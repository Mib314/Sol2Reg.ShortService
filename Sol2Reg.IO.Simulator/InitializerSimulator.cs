﻿// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Simulator\InitializerSimulator.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Simulator\InitializerSimulator.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Simulator\InitializerSimulator.cs
//     Created            : 16.01.2013 14:18
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Simulator
{
	using System.ComponentModel.Composition;
	using Sol2Reg.IO.Interface;

	/// <summary>Initializer for simulator.</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof (IInitializer))]
	public class InitializerSimulator : InitializerBase
	{
		/// <summary>Initialyses the module.</summary>
		/// <remarks>
		///     The <paramref name="moduleSerie" /> and <paramref name="moduleType" /> is not important for the simulator
		/// </remarks>
		public override IModuleBase InitializeModule(string moduleSerie, string moduleType, IModules modules)
		{
			return new SimulatorModule().Initialize(moduleSerie, moduleType, modules);
		}
	}
}