﻿// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO\Modules.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO\Modules.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO\Modules.cs
//     Created            : 28.12.2012 04:43
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO
{
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using Sol2Reg.IO.Interface;

	/// <summary>List of module.</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class Modules :  IModules
	{
		[Import]
		private InitializeModules initializeModules;

		/// <summary>
		///     Initializes a new instance of the <see cref="Modules" /> class.
		/// </summary>
		public Modules()
		{
			this.IsSimulationMode = false;
			this.ModuleList = new List<IModuleBase>();
		}

		#region IModules Members
		/// <summary>Initializes the specified is simulation mode.</summary>
		/// <param name="isSimulationMode" >
		///     if set to <c>true</c> [is simulation mode].
		/// </param>
		/// <returns>Modules list.</returns>
		public IModules Initialize(bool isSimulationMode = false)
		{
			this.IsSimulationMode = isSimulationMode;
			return this;
		}

		/// <summary>Gets or sets the module list.</summary>
		/// <value>The module list.</value>
		public IList<IModuleBase> ModuleList { get; set; }

		/// <summary>Gets or sets a value indicating whether [simulation mode].</summary>
		/// <value>
		///     <c>true</c> if [simulation mode]; otherwise, <c>false</c>.
		/// </value>
		public bool IsSimulationMode { get; private set; }

		/// <summary>Starts all modules connection.</summary>
		public void Start()
		{
			foreach (var module in this.ModuleList)
			{
				module.Start();
			}
		}

		/// <summary>Closes all modules connection.</summary>
		public void Closing()
		{
			foreach (var module in this.ModuleList)
			{
				module.Closing();
			}
		}

		/// <summary>Reads the data from all modules IO. Place the response value to the Channels list.</summary>
		void IModules.ReadData()
		{
			foreach (var module in this.ModuleList)
			{
				module.ReadData();
			}
		}

		/// <summary>Adds a new module.</summary>
		/// <param name="name" >The name.</param>
		/// <param name="moduleType" >Type of the module.</param>
		/// <param name="moduleSerie" >The module serie.</param>
		/// <param name="ipAddress" >The ip address.</param>
		/// <param name="port" >The port.</param>
		/// <param name="chanels" >The chanels.</param>
		public void AddNewModule(string name, string moduleType, string moduleSerie, string ipAddress, int port, List<IChanel> chanels)
		{
			var module = this.initializeModules.AddModule(moduleType, moduleSerie);
			module.Name = name;
			module.IpAddress = ipAddress;
			module.Port = port;
			module.Chanels = chanels;
		}
		#endregion

		/// <summary>Sets the simulation mode.</summary>
		internal void SetSimulationMode()
		{
			this.IsSimulationMode = true;
		}

		public void ReadData()
		{
			foreach (var module in this.ModuleList)
			{
				module.ReadData();
			}
		}

		public void WriteData(string moduleName, int chanelId, float? analogValue, bool? digitalValue)
		{
			var module = this.ModuleList.FirstOrDefault(foo => foo.Name == moduleName);
			if (module == null)
			{
				return;
			}

			var chanel = module.Chanels.FirstOrDefault(foo => foo.Id == chanelId);
			if (chanel == null)
			{
				return;
			}

			chanel.ValueAnalog = analogValue;
			chanel.ValueDigital = digitalValue;
		}
	}
}