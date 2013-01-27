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

namespace Sol2Reg.IO
{
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using Sol2Reg.IO.Interface;
	using Sol2Reg.IO.Simulator;
	using Sol2Reg.Tools.Error;

	/// <summary>
	/// Initialize module
	/// </summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class InitializeModules
	{
		private IInitializer simulatorInitializer;

		[ImportMany(typeof (IInitializer))]
		public IEnumerable<IInitializer> Initializers { get; set; }

		[Import]
		public LoadModuleSetting LoadModuleSetting { get; set; }

		[Import]
		public ErrorTracking ErrorTracking { get; set; }

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
		public InitializeModules Initialize(bool isForSimulator = false)
		{
			this.IsForSimulator = isForSimulator;

			if (this.IsForSimulator)
			{
				this.simulatorInitializer = this.Initializers.FirstOrDefault(foo => foo.ModuleSerie_Key == typeof (InitializerSimulator).ToString());
				if (this.simulatorInitializer == null)
				{
					this.ErrorTracking.Add(ErrorIdList.InitilizeModule_NoSimulatorInitializer, ErrorGravity.FatalApplication);
					return null;
				}
				// Load config module settings. 
				this.Modules = this.LoadModuleSetting.LoadConfig(this.InitialiseModuleSimulator);
			}
			else
			{
				// Load config module settings. 
				this.Modules = this.LoadModuleSetting.LoadConfig(this.InitialiseModuleProductive);
			}
			return this;
		}

		/// <summary>
		/// Initialises the module simulator.
		/// </summary>
		/// <param name="moduleSerie">The module serie.</param>
		/// <param name="moduleType">Type of the module.</param>
		/// <returns>Modules.</returns>
		private IModuleBase InitialiseModuleSimulator(string moduleSerie, string moduleType)
		{
			return this.simulatorInitializer.InitializeModule(moduleSerie, moduleType, this.Modules);
		}

		/// <summary>
		/// Initialises the module productive.
		/// Find in the Initializer for this Module serie and initialise the module.
		/// </summary>
		/// <param name="moduleSerie">The module serie.</param>
		/// <param name="moduleType">Type of the module.</param>
		/// <returns>The moduleBase implementation for this ModuleSerie.</returns>
		private IModuleBase InitialiseModuleProductive(string moduleSerie, string moduleType)
		{
			var initializer = this.Initializers.FirstOrDefault(foo => foo.ModuleSerie_Key == moduleSerie);

			if (initializer == null)
			{
				this.ErrorTracking.Add(ErrorIdList.InitilizeModule_NoProductiveInitializer, ErrorGravity.FatalApplication);
				return null;
			}

			return initializer.InitializeModule(moduleSerie, moduleType, this.Modules);
		}

		/// <summary>Adds the module.</summary>
		/// <param name="moduleType" >Type of the module.</param>
		/// <param name="moduleSerie" >The module serie.</param>
		/// <returns>The module.</returns>
		public IModuleBase AddModule(string moduleType, string moduleSerie)
		{
			return this.IsForSimulator ? this.InitialiseModuleSimulator(moduleSerie, moduleType) : this.InitialiseModuleProductive(moduleSerie, moduleType);
		}
	}
}