// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO\InitializeModules.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO\InitializeModules.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO\InitializeModules.cs
//     Created            : 28.12.2012 15:26
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ModuleIO
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using ModuleIO.Interface;
	using Sol2Reg.IO.Simulator;

	/// <summary>Initialize module</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class InitializeModules
	{
		private IInitializer simulatorInitializer;

		[ImportMany(typeof(IInitializer))]
		private List<IInitializer> initializers;

		/// <summary>Gets the modules.</summary>
		/// <value>The modules.</value>
		public IModules Modules { get; private set; }

		/// <summary>Gets or sets a value indicating whether this instance is for simulator.</summary>
		/// <value>
		///     <c>true</c> if this instance is for simulator; otherwise, <c>false</c>.
		/// </value>
		public bool IsForSimulator { get; private set; }

		/// <summary>Initializes the specified is for simulator.</summary>
		/// <param name="isForSimulator" >
		///     if set to <c>true</c> [is for simulator].
		/// </param>
		/// <exception cref="System.ArgumentNullException" >The simulator module is not present. Please add the \Sol2Reg.IO.Simulator\ DLL module in your application folder.</exception>
		public InitializeModules Initialize(bool isForSimulator = false)
		{
			this.IsForSimulator = isForSimulator;
			this.Modules = new LoadModuleSetting().LoadConfig(this.InitializeModule);

			if (this.IsForSimulator)
			{
				this.simulatorInitializer = this.initializers.FirstOrDefault(foo => foo.ModuleSerie_Key == InitializerSimulator.Module_Simulator);
				if (this.simulatorInitializer == null) throw new ArgumentNullException("", "The simulator module is not present. Please add the \"Sol2Reg.IO.Simulator.DLL\" module in your application folder.");
			}

			return this;
		}


		/// <summary>Initialises the module.</summary>
		/// <param name="moduleSerie" >The module serie.</param>
		/// <param name="moduleType" >Type of the module.</param>
		/// <returns>The module initialized</returns>
		/// <exception cref="System.ArgumentOutOfRangeException" >If moduleSerie or moduleType not exist.</exception>
		private IModuleBase InitializeModule(string moduleSerie, string moduleType)
		{
			return this.IsForSimulator ? this.InitialiseModuleSimulator(moduleSerie, moduleType) : this.InitialiseModuleProductive(moduleSerie, moduleType);
		}

		private IModuleBase InitialiseModuleSimulator(string moduleSerie, string moduleType)
		{
			return this.simulatorInitializer.InitializeModule(moduleSerie, moduleType, this.Modules);
		}

		private IModuleBase InitialiseModuleProductive(string moduleSerie, string moduleType)
		{
			var initializer = this.initializers.FirstOrDefault(foo => foo.ModuleSerie_Key == moduleSerie);

			if (initializer == null) throw new ArgumentNullException("", string.Format("The {0} module is not present. Please add this DLL module in your application folder.", moduleSerie));

			return initializer.InitializeModule(moduleSerie, moduleType, this.Modules);
		}

		/// <summary>
		/// Adds the module.
		/// </summary>
		/// <param name="moduleType">Type of the module.</param>
		/// <param name="moduleSerie">The module serie.</param>
		/// <returns>The module.</returns>
		public IModuleBase AddModule(string moduleType, string moduleSerie)
		{
			return this.IsForSimulator ? this.InitialiseModuleSimulator(moduleSerie, moduleType) : this.InitialiseModuleProductive(moduleSerie, moduleType);
		}
	}
}