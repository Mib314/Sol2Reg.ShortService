// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ADAM6000Com\Adam60Xx.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ADAM6000Com\Adam60Xx.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ADAM6000Com\Adam60Xx.cs
//     Created            : 18.12.2012 00:18
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ADAM6000Com
{
	using System;
	using System.Linq;
	using System.Net.Sockets;
	using Advantech.Adam;
	using Advantech.Common;
	using ModuleIO.Interface;
	using Sol2Reg.ServiceData.Enumerations;

	public abstract class Adam60Xx : ModuleBase, IAdamModule
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Adam60Xx" /> class.
		/// </summary>
		protected Adam60Xx()
		{
			this.Port = 502; // modbus TCP port is 502
			this.AdamModbus = new AdamSocket();
			this.AdamModbus.SetTimeout(1000, 1000, 1000); // set timeout for TCP
		}


		#region Properties
		/// <summary>Gets the module serie.</summary>
		/// <value>The module serie.</value>
		public override string ModuleSerie
		{
			get { return "Adam6000Type"; }
			protected set { }
		}

		/// <summary>Gets the type of the module.</summary>
		/// <value>The type of the module.</value>
		public override string ModuleType
		{
			get { return this.Adam6000Type.ToString(); }
			protected set { }
		}

		/// <summary>Gets a value indicating whether this instance is connected.</summary>
		/// <value>
		///     <c>true</c> if this instance is connected; otherwise, <c>false</c>.
		/// </value>
		public override sealed bool IsConnected
		{
			get { return this.AdamModbus.Connected; }
			protected set { }
		}


		/// <summary>Gets or sets the adam modbus.</summary>
		/// <value>The adam modbus.</value>
		internal AdamSocket AdamModbus { get; set; }

		/// <summary>Gets or sets the count.</summary>
		/// <value>The count.</value>
		internal int Count { get; set; }

		/// <summary>Gets or sets the chanel enabled.</summary>
		/// <value>The chanel enabled.</value>
		internal bool[] ChanelEnabled { get; set; }

		/// <summary>Gets or sets the by range input.</summary>
		/// <value>The by range input.</value>
		internal byte[] ByRangeInput { get; set; }

		/// <summary>Gets or sets the by range output.</summary>
		/// <value>The by range output.</value>
		internal byte[] ByRangeOutput { get; set; }

		/// <summary>Gets the type of the adam6000.</summary>
		/// <value>The type of the adam6000.</value>
		public Adam6000Type Adam6000Type { get; internal set; }

		/// <summary>Gets the last error.</summary>
		/// <value>The last error.</value>
		public ErrorCode LastError
		{
			get { return this.AdamModbus.LastError; }
		}

		/// <summary>Gets the total chanel analaog in.</summary>
		/// <value>The total chanel analaog in.</value>
		public int TotalChanelAnalaogIn { get; internal set; }

		/// <summary>Gets the total chanel analaog out.</summary>
		/// <value>The total chanel analaog out.</value>
		public int TotalChanelAnalaogOut { get; internal set; }

		/// <summary>Gets the total chanel digital out.</summary>
		/// <value>The total chanel digital out.</value>
		public int TotalChanelDigitalOut { get; internal set; }

		/// <summary>Gets the total chanel digital in.</summary>
		/// <value>The total chanel digital in.</value>
		public int TotalChanelDigitalIn { get; internal set; }

		/// <summary>Gets the id sart for input chanel.</summary>
		/// <value>The id sart for input chanel.</value>
		public int IdSartForInputChanel { get; internal set; }

		/// <summary>Gets the id sart for output chanel.</summary>
		/// <value>The id sart for output chanel.</value>
		public int IdSartForOutputChanel { get; internal set; }
		#endregion

		public override void Start()
		{
			if (this.IsConnected) return;
			var lastError = this.AdamModbus.LastError;
			if (this.AdamModbus.Connect(this.IpAddress, ProtocolType.Tcp, this.Port)) this.Count = 0; // reset the reading counter
			else base.AddModuleError((ModuleErrorCode) this.AdamModbus.LastError);
			try
			{
				if (this.TotalChanelAnalaogIn > 0)
				{
					foreach (var chanelData in this.Chanels.Where(foo => foo.Direction == Direction.Input))
					{
						byte byRange;
						if (this.AdamModbus.AnalogInput().GetInputRange(chanelData.Id, out byRange)) this.ByRangeInput[chanelData.Id] = byRange;
						else this.AddModuleError(chanelData, (ModuleErrorCode) this.AdamModbus.LastError);
					}
				}
			}
			catch
			{
				this.AddModuleError(ModuleErrorCode.Module_StartChanel_Error);
			}
		}

		public override void Closing()
		{
			if (this.IsConnected) this.AdamModbus.Disconnect(); // disconnect slave
		}

		protected void InitializeAdamInternValue()
		{
			// Trouve le nombre de canals Analog et Digital en Input et Output.
			this.TotalChanelAnalaogIn = AnalogInput.GetChannelTotal(this.Adam6000Type);
			this.TotalChanelAnalaogOut = AnalogOutput.GetChannelTotal(this.Adam6000Type);
			this.TotalChanelDigitalIn = DigitalInput.GetChannelTotal(this.Adam6000Type);
			this.TotalChanelDigitalOut = DigitalOutput.GetChannelTotal(this.Adam6000Type);

			this.IsInitialized = true;
		}

		internal void CheckIfModuleIsavailableToCommunicate()
		{
			if (!this.IsInitialized) throw new Exception(string.Format("The module [{0}] is not initialized.", this.Name));
			if (!this.IsConnected) throw new Exception(string.Format("The module [{0}] is not connected.", this.Name));
		}
	}
}