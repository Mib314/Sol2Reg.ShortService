// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ADAM6000Com\Adam6015.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ADAM6000Com\Adam6015.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ADAM6000Com\Adam6015.cs
//     Created            : 17.12.2012 16:41
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ADAM6000Com
{
	using System;
	using Advantech.Adam;
	using ModuleIO.Interface;

	public sealed class Adam6015 : Adam60Xx
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Adam6015" /> class.
		/// </summary>
		public Adam6015()
		{
			// Cette class ne s'occupe que de l'Adam6015
			this.Adam6000Type = Adam6000Type.Adam6015;
			this.InitializeAdamInternValue();
		}

		/// <summary>Initialize the module.</summary>
		public override IModuleBase Initialize(string moduleSerie, string moduleType, IModules modules)
		{
			base.InitializeAdamInternValue();

			this.ChanelEnabled = new bool[this.TotalChanelAnalaogIn];
			this.ByRangeInput = new byte[this.TotalChanelAnalaogIn];
			return this.InitializeBase(modules);
		}

		public override void ReadData()
		{
			base.CheckIfModuleIsAwiableToCommunicate();

			this.Count++; // increment the reading counter
			this.RefreshChannelValue();
		}

		public override void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null)
		{
			base.CheckIfModuleIsAwiableToCommunicate();

			throw new NotImplementedException();
		}

		private void RefreshChannelValue()
		{
			const int START = 1;
			const int BURN_START = 121;
			int[] iData;

			// Lit les données des inputs et les place dans iData (Valeur brute)
			if (this.AdamModbus.Modbus().ReadInputRegs(START, this.TotalChanelAnalaogIn, out iData))
			{
				foreach (var chanelData in this.Chanels)
				{
					// Convertit chaque chanel de la valeur brut en une valeur util
					chanelData.ValueAnalog =
						(AnalogInput.GetScaledValue(this.Adam6000Type, this.ByRangeInput[chanelData.Id], iData[chanelData.Id])*chanelData.Gain) +
						chanelData.Offset;
				}
				// check if the value is not over tem range (ReadCoilStatus).
				//if (Adam6000Type == Adam6000Type.Adam6015)
				//{
				bool[] bBurn;
				if (this.AdamModbus.Modbus().ReadCoilStatus(BURN_START, this.TotalChanelAnalaogIn, out bBurn)) // read burn out flag
				{
					foreach (var chanelData in this.Chanels)
					{
						if (bBurn[chanelData.Id]) chanelData.ValueAnalog = float.MinValue;
					}
				}
				//}
			}
		}
	}
}