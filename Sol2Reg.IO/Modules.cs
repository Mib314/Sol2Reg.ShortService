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
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using ModuleIO.Interface;

	/// <summary>List of module.</summary>
	public class Modules : List<IModuleBase>, IModules
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Modules" /> class.
		/// </summary>
		public Modules(bool setSimulatorMode = false)
		{
			this.SimulationMode = setSimulatorMode;
		}

		#region IModules Members
		/// <summary>Gets a value indicating whether [simulation mode].</summary>
		/// <value>
		///     <c>true</c> if [simulation mode]; otherwise, <c>false</c>.
		/// </value>
		public bool SimulationMode { get; private set; }

		/// <summary>Starts all modules connection.</summary>
		public void Start()
		{
			foreach (var module in this)
			{
				module.Start();
			}
		}

		/// <summary>Closes all modules connection.</summary>
		public void Closing()
		{
			foreach (var module in this)
			{
				module.Closing();
			}
		}

		/// <summary>Reads the data from all modules IO. Place the response value to the Channels list.</summary>
		void IModules.ReadData()
		{
			foreach (var module in this)
			{
				module.ReadData();
			}
		}

		/// <summary>Adds the new module.</summary>
		/// <param name="name" >The name.</param>
		/// <param name="moduleType" >Type of the module.</param>
		/// <param name="moduleSerie" >The module serie.</param>
		/// <param name="ipAddress" >The ip address.</param>
		/// <param name="port" >The port.</param>
		/// <param name="chanels" >The chanels.</param>
		public void AddNewModule(string name, string moduleType, string moduleSerie, string ipAddress, int port, List<IChanel> chanels)
		{
			throw new NotImplementedException();
		}
		#endregion

		/// <summary>Sets the simulation mode.</summary>
		internal void SetSimulationMode()
		{
			this.SimulationMode = true;
		}

		public void ReadData()
		{
			foreach (var module in this)
			{
				module.ReadData();
			}
		}

		public void WriteData(string moduleName, int chanelId, float? analogValue, bool? digitalValue)
		{
			var module = this.FirstOrDefault(foo => foo.Name == moduleName);
			if (module == null) return;

			var chanel = module.Chanels.FirstOrDefault(foo => foo.Id == chanelId);
		}
	}
}