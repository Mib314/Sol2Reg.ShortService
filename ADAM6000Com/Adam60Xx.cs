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
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Sockets;
	using Advantech.Adam;
	using Advantech.Common;
	using ModuleIO.Interface;
	using ModuleIO_Interface;

	public abstract class Adam60Xx : IModule
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="Adam60Xx" /> class.
		/// </summary>
		protected Adam60Xx()
		{
			this.Port = 502; // modbus TCP port is 502
			this.AdamModbus = new AdamSocket();
			this.AdamModbus.SetTimeout(1000, 1000, 1000); // set timeout for TCP
			this.Chanels = new List<IChanelData>();
		}

		#region Properties
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

		/// <summary>Gets or sets the name.</summary>
		/// <value>The name.</value>
		public string Name { get; set; }

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

		/// <summary>Gets or sets the ip address.</summary>
		/// <value>The ip address.</value>
		public string IpAddress { get; set; }

		public int Port { get; set; }

		/// <summary>Gets a value indicating whether this instance is connected.</summary>
		/// <value>
		///     <c>true</c> if this instance is connected; otherwise, <c>false</c>.
		/// </value>
		public bool IsConnected
		{
			get { return this.AdamModbus.Connected; }
		}

		/// <summary>Gets the chanels.</summary>
		/// <value>The chanels.</value>
		public IList<IChanelData> Chanels { get; internal set; }
		#endregion

		#region IModule Members
		public void Start()
		{
			if (this.IsConnected) return;
			if (this.LastError != ErrorCode.No_Error) this.AdamModbus.Disconnect();

			if (this.AdamModbus.Connect(this.IpAddress, ProtocolType.Tcp, this.Port)) this.Count = 0; // reset the reading counter
			try
			{
				if (this.TotalChanelAnalaogIn > 0)
				{
					foreach (var chanelData in this.Chanels.Where(foo => foo.Direction == Direction.Input))
					{
						byte byRange;
						if (this.AdamModbus.AnalogInput().GetInputRange(chanelData.Id, out byRange)) this.ByRangeInput[chanelData.Id] = byRange;
					}
				}
			}
			catch {}
		}

		public void Closing()
		{
			if (this.IsConnected) this.AdamModbus.Disconnect(); // disconnect slave
		}

		/// <summary>Read data from ADAM module to the chanels list.</summary>
		public abstract void ReadData();

		/// <summary>Write new info to the chanel list and change the value to the ADAM module</summary>
		/// <param name="chanelId" >Chanel Id.</param>
		/// <param name="digitalValue" >Digital value (true/false).</param>
		/// <param name="anamlogValue" >Analog value.</param>
		public abstract void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null);
		#endregion

		internal void InitialyseModule()
		{
			// Trouve le nombre de canals Analog et Digital en Input et Output.
			this.TotalChanelAnalaogIn = AnalogInput.GetChannelTotal(this.Adam6000Type);
			this.TotalChanelAnalaogOut = AnalogOutput.GetChannelTotal(this.Adam6000Type);
			this.TotalChanelDigitalIn = DigitalInput.GetChannelTotal(this.Adam6000Type);
			this.TotalChanelDigitalOut = DigitalOutput.GetChannelTotal(this.Adam6000Type);
		}
	}
}