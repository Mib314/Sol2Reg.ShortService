// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Service\Sol2RegService.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Service\Sol2RegService.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Service\Sol2RegService.cs
//     Created            : 31.12.2012 15:51
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Service
{
	using System;
	using System.ComponentModel.Composition;
	using System.Linq;
	using Sol2Reg.IO;
	using Sol2Reg.IO.Interface;
	using Sol2Reg.ServiceData.ServiceData;

	/// <summary>
	/// Sol2Reg service.
	/// </summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export(typeof(ISol2RegService))]
	public class Sol2RegService : ISol2RegService
	{
		private IModules modules;

		[Import]
		private InitializeModules initializeModules;

		public void Initilize(bool isForSimulation = false)
		{
			this.modules = initializeModules.Initialize(isForSimulation).Modules;
		}

		#region ISol2RegService Members
		/// <summary>Initilizes the connection.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		public void InitilizeConnection(string moduleName)
		{
			this.modules.Start();
		}

		/// <summary>Closes the connection.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		public void CloseConnection(string moduleName)
		{
			this.modules.Closing();
		}

		/// <summary>Reads the data.</summary>
		/// <returns>Modules value.</returns>
		public ModulesData ReadData()
		{
			var modulesData = new ModulesData();

			//Ask to read data to evry module IO.
			this.modules.ReadData();
			// Scan evry module to copy data.
			foreach (var module in this.modules)
			{
				//Transform chanels => ChanelData to transfert by WCF.
				var chanelsData = module.Chanels.Select(chanel => new ChanelData {Id = chanel.Id, Key = chanel.Key, ValueAnalog = chanel.ValueAnalog, ValueDigital = chanel.ValueDigital}).ToList();

				// Add to module liste
				modulesData.Modules.Add(new ModuleData {Name = module.Name, IpAddress = module.IpAddress, ModuleType = module.ModuleType, ModuleSerie = module.ModuleSerie, Chanels = chanelsData});
			}
			return modulesData;
		}

		/// <summary>Writes the data.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		/// <param name="chanelId" >The chanel id.</param>
		/// <param name="analogValue" >The analog value.</param>
		/// <param name="digitalValue" >The digital value.</param>
		public void WriteData(string moduleName, int chanelId, float? analogValue, bool? digitalValue)
		{
			var module = this.modules.FirstOrDefault(foo => foo.Name == moduleName);

			if (module != null) module.WriteData(chanelId, digitalValue, analogValue);
		}

		/// <summary>Reads the config in memory.</summary>
		/// <returns>The config of all modules.</returns>
		public ModulesData ReadConfig()
		{
			var modulesData = new ModulesData();

			// Scan evry module to copy data.
			foreach (var module in this.modules)
			{
				//Transform chanels => ChanelData to transfert by WCF.
				var chanelsData = module.Chanels.Select(chanel => new ChanelData {Id = chanel.Id, Key = chanel.Key, ValueAnalog = chanel.ValueAnalog, ValueDigital = chanel.ValueDigital, ValueAnalogBrut = chanel.ValueAnalogBrut, Gain = chanel.Gain, Offset = chanel.Offset, TypeOfValue = chanel.TypeOfValue, Direction = chanel.Direction, Description = chanel.Description, Comment = chanel.Comment}).ToList();

				// Add to module liste
				modulesData.Modules.Add(new ModuleData {Name = module.Name, IpAddress = module.IpAddress, ModuleType = module.ModuleType, ModuleSerie = module.ModuleSerie, Chanels = chanelsData});
			}
			return modulesData;
		}

		/// <summary>Adds the new module.</summary>
		/// <param name="newModule" >The new module.</param>
		public void AddNewModule(ModuleData newModule)
		{
			// Build module data

			var chanels = newModule.Chanels.Select(chanelData => new Chanel(chanelData.Id, chanelData.Key, chanelData.Direction, chanelData.TypeOfValue) {Gain = chanelData.Gain, Offset = chanelData.Offset, Description = chanelData.Description, Comment = chanelData.Comment}).Cast<IChanel>().ToList();

			this.modules.AddNewModule(newModule.Name, newModule.ModuleType, newModule.ModuleSerie, newModule.IpAddress, newModule.Port, chanels);
		}

		/// <summary>Deletes the module.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		public void DeleteModule(string moduleName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Changes the module info.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		/// <param name="moduleNewName" >New name of the module.</param>
		/// <param name="newIpAddress" >The new ip address.</param>
		/// <param name="newPort" >The new port.</param>
		public void ChangeModuleInfo(string moduleName, string moduleNewName, string newIpAddress, int? newPort)
		{
			throw new NotImplementedException();
		}

		/// <summary>Changes the chanel value correction.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		/// <param name="chanelId" >The chanel id.</param>
		/// <param name="newGain" >The new gain.</param>
		/// <param name="newOffset" >The new offset.</param>
		public void ChangeChanelValueCorrection(string moduleName, int chanelId, float newGain, float newOffset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Changes the chanel info.</summary>
		/// <param name="moduleName" >Name of the module.</param>
		/// <param name="chanelId" >The chanel id.</param>
		/// <param name="newName" >The new name.</param>
		/// <param name="newDesctiprion" >The new desctiprion.</param>
		/// <param name="newComment" >The new comment.</param>
		public void ChangeChanelInfo(string moduleName, int chanelId, string newName, string newDesctiprion, string newComment)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves the config change.</summary>
		public void SaveConfigChange()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}