// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleSimulator\SimulatorModule.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleSimulator\SimulatorModule.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleSimulator\SimulatorModule.cs
//     Created            : 28.12.2012 11:37
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Simulator
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using ModuleIO.Interface;
	using ModuleIO.Interface.Simulator;
	using Sol2Reg.ServiceData;
	using Sol2Reg.ServiceData.Enumerations;

	public sealed class SimulatorModule : ModuleBase, ISimulatorModule
	{
#pragma warning disable 649
		private IDictionary<int, float?> chanelAnalogInputState;
		private IDictionary<int, bool?> chanelDigitalInputState;
		private IDictionary<int, float?> chanelAnalogOutputState;
		private IDictionary<int, bool?> chanelDigitalOututState;
#pragma warning restore 649


		/// <summary>Initialyses the module.</summary>
		public void InitialyseModule()
		{
			foreach (var chanelData in this.Chanels.OrderBy(foo => foo.Id))
			{
				if (chanelData.Direction == Direction.Input)
				{
					if (chanelData.TypeOfValue == TypeOfValue.Analog) this.chanelAnalogInputState.Add(chanelData.Id, chanelData.ValueAnalogBrut);
					else this.chanelDigitalInputState.Add(chanelData.Id, chanelData.ValueDigital);
				}
				else
				{
					if (chanelData.TypeOfValue == TypeOfValue.Analog) this.chanelAnalogOutputState.Add(chanelData.Id, chanelData.ValueAnalogBrut);
					else this.chanelDigitalOututState.Add(chanelData.Id, chanelData.ValueDigital);
				}
			}
		}

		#region IModuleBase Members
		/// <summary>Gets the module serie.</summary>
		/// <value>The module serie.</value>
		public override string ModuleSerie { get; protected set; }

		/// <summary>Gets the type of the module.</summary>
		/// <value>The type of the module.</value>
		public override string ModuleType { get; protected set; }

		/// <summary>Gets or sets the errors.</summary>
		/// <value>The errors.</value>
		public IModuleErrors ModuleErrors { get; set; }

		/// <summary>
		/// Initialize the module.
		/// </summary>
		/// <param name="moduleSerie">The module serie.</param>
		/// <param name="moduleType">Type of the module.</param>
		/// <param name="modules">The modules.</param>
		/// <returns>
		/// Self instance.
		/// </returns>
		public override IModuleBase Initialize(string moduleSerie, string moduleType, IModules modules)
		{
			this.ModuleSerie = moduleSerie;
			this.ModuleType = moduleType;
			return this.InitializeBase(modules);
		}

		/// <summary>Connect the module.</summary>
		public override void Start()
		{
			if (string.IsNullOrWhiteSpace(this.IpAddress) ||
				this.Port < 1) this.IsConnected = false;
			this.IsConnected = true;
		}

		/// <summary>Closings module connection.</summary>
		public override void Closing()
		{
			this.IsConnected = false;
		}

		/// <summary>Reads the data from the module IO. Place the response value to the Channels list.</summary>
		public override void ReadData()
		{
			// Read all input chanel for this chamels list.
			foreach (var chanelData in this.chanelAnalogInputState)
			{
				var chanelToChange = this.Chanels.FirstOrDefault(foo => foo.Id == chanelData.Key);
				if (chanelToChange != null) chanelToChange.ValueAnalogBrut = chanelData.Value;
			}
			foreach (var chanelData in this.chanelDigitalInputState)
			{
				var chanelToChange = this.Chanels.FirstOrDefault(foo => foo.Id == chanelData.Key);
				if (chanelToChange != null) chanelToChange.ValueDigital = chanelData.Value;
			}
		}

		/// <summary>Write new info to the chanel list and change the value to the ADAM module</summary>
		/// <param name="chanelId" >Chanel Id.</param>
		/// <param name="digitalValue" >Digital value (true/false).</param>
		/// <param name="anamlogValue" >Analog value.</param>
		/// <exception cref="System.IndexOutOfRangeException" >chanelId</exception>
		public override void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null)
		{
			// Write new value for output chanel.
			if (this.chanelAnalogOutputState.ContainsKey(chanelId)) this.chanelAnalogOutputState[chanelId] = anamlogValue;
			else if (this.chanelDigitalOututState.ContainsKey(chanelId)) this.chanelDigitalOututState[chanelId] = digitalValue;

			throw new IndexOutOfRangeException("chanelId");
		}
		#endregion

		/// <summary>Injects the new analog input value.</summary>
		/// <param name="idChanel" >The id chanel.</param>
		/// <param name="value" >The value.</param>
		/// <exception cref="System.NotImplementedException" ></exception>
		public void InjectNewAnalogInputValue(int idChanel, float? value)
		{
			if (this.chanelAnalogInputState.ContainsKey(idChanel)) this.chanelAnalogInputState[idChanel] = value;
			else throw new ArgumentOutOfRangeException("idChanel", "This chanel is not a Analog Input channel");
		}

		/// <summary>Injects the new digital input value.</summary>
		/// <param name="idChanel" >The id chanel.</param>
		/// <param name="value" >
		///     if set to <c>true</c> [value].
		/// </param>
		/// <exception cref="System.NotImplementedException" ></exception>
		public void InjectNewDigitalInputValue(int idChanel, bool? value)
		{
			if (this.chanelDigitalInputState.ContainsKey(idChanel)) this.chanelDigitalInputState[idChanel] = value;
			else throw new ArgumentOutOfRangeException("idChanel", "This chanel is not a Analog Input channel");
		}
	}
}