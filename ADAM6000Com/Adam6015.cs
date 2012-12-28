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
	using System.Collections.Generic;
	using Advantech.Adam;
	using ModuleIO;
	using ValueType = ModuleIO.ValueType;

	public class Adam6015 : Adam60Xx
	{
		public Adam6015()
		{
			// Cette class ne s'occupe que de l'Adam6015
			base.Adam6000Type = Adam6000Type.Adam6015;

			this.IpAddress = "192.168.200.151"; // modbus slave IP address
			this.Port = 502; // modbus TCP port is 502
			this.AdamModbus = new AdamSocket();
			this.AdamModbus.SetTimeout(1000, 1000, 1000); // set timeout for TCP

			this.InitialyseModule();

			this.ChanelEnabled = new bool[this.TotalChanelAnalaogIn];
			this.ByRangeInput = new byte[this.TotalChanelAnalaogIn];

			this.Chanels = new List<ChanelData>
						   {
							   new ChanelData(0, "TS1", Direction.Input, ValueType.Analog) {Description = "Temp. absorbeur 1"},
							   new ChanelData(1, "TB1", Direction.Input, ValueType.Analog) {Description = "Temp. Ballon 1 (bas)"},
							   new ChanelData(2, "TB2", Direction.Input, ValueType.Analog) {Description = "Temp. Ballon 2 (haut)"},
							   new ChanelData(3, "TSD", Direction.Input, ValueType.Analog) {Description = "Temp. de départ vers les absorbeurs"},
							   new ChanelData(4, "TSR", Direction.Input, ValueType.Analog) {Description = "Temp. de retour des absorbeur"},
							   new ChanelData(5, "TBO", Direction.Input, ValueType.Analog) {Description = "Temp. de l'appoint bois (momo)"},
							   new ChanelData(6, "TCh", Direction.Input, ValueType.Analog) {Description = "Temp. départ chauffage mural", Gain = 1, Offset = 0}
						   };
		}

		public override void ReadData()
		{
			this.Count++; // increment the reading counter
			this.RefreshChannelValue();
		}

		public override void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null)
		{
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