// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ADAM6000Com\Adam6066.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ADAM6000Com\Adam6066.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ADAM6000Com\Adam6066.cs
//     Created            : 18.12.2012 00:18
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ADAM6000Com
{
	using System;
	using System.Linq;
	using Advantech.Adam;
	using ModuleIO.Interface;

	/// <summary>
	/// Adam 6066 for digital input and output (220V)
	/// </summary>
	public sealed class Adam6066 : Adam60Xx
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Adam6066" /> class.
		/// </summary>
		public Adam6066()
		{
			// Cette class ne s'occupe que de l'Adam6066
			this.Adam6000Type = Adam6000Type.Adam6066;

			this.InitializeAdamInternValue();

			this.IdSartForInputChanel = 1;
			this.IdSartForOutputChanel = 17;
		}

		/// <summary>Initialize the module.</summary>
		public override IModuleBase Initialize(string moduleSerie, string moduleType, IModules modules)
		{
			base.InitializeAdamInternValue();
			this.ChanelEnabled = new bool[this.TotalChanelDigitalIn + this.TotalChanelDigitalOut];
			this.ByRangeInput = new byte[this.TotalChanelDigitalIn + this.TotalChanelDigitalOut];

			return this.InitializeBase(modules);
		}

		/// <summary>Read data from ADAM module to the chanel list.</summary>
		public override void ReadData()
		{
			base.CheckIfModuleIsAwiableToCommunicate();

			bool[] bDiData, bDoData;
			if (
				this.AdamModbus.Modbus().ReadCoilStatus(this.IdSartForInputChanel, this.TotalChanelDigitalIn, out bDiData) &&
				this.AdamModbus.Modbus().ReadCoilStatus(this.IdSartForOutputChanel, this.TotalChanelDigitalOut, out bDoData))
			{
				//Update status
				var chanelId = 0;
				foreach (var coilStatus in bDiData)
				{
					this.Chanels[chanelId].ValueDigital = coilStatus;
					chanelId++;
				}
				foreach (var coilStatus in bDoData)
				{
					this.Chanels[chanelId].ValueDigital = coilStatus;
					chanelId++;
				}
			}
		}

		public override void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null)
		{
			base.CheckIfModuleIsAwiableToCommunicate();

			if (chanelId < 6 ||
				chanelId > 11) throw new ArgumentOutOfRangeException("chanelId");

			// Les chanels en Out commence par 17 (voir doc ADAM 6000 page 243).
			int iOnOff, iStart = this.IdSartForOutputChanel + chanelId - this.TotalChanelDigitalIn;

			var chanel = this.Chanels.FirstOrDefault(foo => foo.Id == chanelId);
			if (chanel == null) throw new ArgumentOutOfRangeException("chanelId");


			iOnOff = chanel.ValueDigital ?? false ? 0 : 1;

			if (this.AdamModbus.Modbus().ForceSingleCoil(iStart, iOnOff)) chanel.ValueDigital = !chanel.ValueDigital;
			else throw new Exception(string.Format("Module {0}, Chanel {1} cant assigne his digital value.", this.Adam6000Type, chanel.Id));
		}
	}
}