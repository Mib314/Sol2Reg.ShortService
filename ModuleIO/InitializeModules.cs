﻿// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO\InitializeModules.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO\InitializeModules.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO\InitializeModules.cs
//     Created            : 28.12.2012 15:26
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ModuleIO
{
	using System;
	using ADAM6000Com;
	using ModuleIO.Interface;
	using ModuleSimulator;

	public class InitializeModules
	{
		private readonly LoadModulSetting loadModulSetting;

		/// <summary>
		///     Initializes a new instance of the <see cref="InitializeModules" /> class.
		/// </summary>
		/// <param name="isForSimulator" >
		///     if set to <c>true</c> [is for simulator].
		/// </param>
		public InitializeModules(bool isForSimulator = false)
		{
			this.loadModulSetting = new LoadModulSetting();

			this.loadModulSetting.LoadConfig(this.InitialiseModule);
		}

		/// <summary>Gets or sets a value indicating whether this instance is for simulator.</summary>
		/// <value>
		///     <c>true</c> if this instance is for simulator; otherwise, <c>false</c>.
		/// </value>
		public bool IsForSimulator { get; private set; }


		/// <summary>Initialises the module.</summary>
		/// <param name="moduleSerie" >The module serie.</param>
		/// <param name="moduleType" >Type of the module.</param>
		/// <returns>The module initialized</returns>
		/// <exception cref="System.ArgumentOutOfRangeException" >If moduleSerie or moduleType not exist.</exception>
		private IModuleBase InitialiseModule(string moduleSerie, string moduleType)
		{
			return this.IsForSimulator ? this.InitialiseModuleSimularor(moduleSerie, moduleType) : this.InitialiseModuleProductive(moduleSerie, moduleType);
		}

		private IModuleBase InitialiseModuleSimularor(string moduleSerie, string moduleType)
		{
			return new SimulatorModule(moduleSerie, moduleType);
		}

		private IModuleBase InitialiseModuleProductive(string moduleSerie, string moduleType)
		{
			switch (moduleSerie)
			{
				case "Adam6000Type":
				{
					switch (moduleType)
					{
						case "Adam6015":
							return new Adam6015();
						case "Adam6066":
							return new Adam6066();
						default:
							throw new ArgumentOutOfRangeException(moduleType);
					}
				}
				default:
				{
					throw new ArgumentOutOfRangeException(moduleSerie);
				}
			}
		}
	}
}