﻿using System.Collections.Generic;

namespace ModuleIO
{
	/// <summary>
	/// Interface to define ModuleIO
	/// </summary>
	public interface IModuleIO
	{
		#region Properties
		/// <summary>
		/// Gets the total chanel analaog in.
		/// </summary>
		/// <value>
		/// The total chanel analaog in.
		/// </value>
		int TotalChanelAnalaogIn { get; }

		/// <summary>
		/// Gets the total chanel analaog out.
		/// </summary>
		/// <value>
		/// The total chanel analaog out.
		/// </value>
		int TotalChanelAnalaogOut { get; }

		/// <summary>
		/// Gets the total chanel digital out.
		/// </summary>
		/// <value>
		/// The total chanel digital out.
		/// </value>
		int TotalChanelDigitalOut { get; }

		/// <summary>
		/// Gets the total chanel digital in.
		/// </summary>
		/// <value>
		/// The total chanel digital in.
		/// </value>
		int TotalChanelDigitalIn { get; }

		/// <summary>
		/// Gets the id sart for input chanel.
		/// </summary>
		/// <value>
		/// The id sart for input chanel.
		/// </value>
		int IdSartForInputChanel { get; }

		/// <summary>
		/// Gets the id sart for output chanel.
		/// </summary>
		/// <value>
		/// The id sart for output chanel.
		/// </value>
		int IdSartForOutputChanel { get; }

		/// <summary>
		/// Gets or sets the ip address.
		/// </summary>
		/// <value>
		/// The ip address.
		/// </value>
		string IpAddress { get; set; }

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		/// <value>
		/// The port.
		/// </value>
		int Port { get; set; }

		/// <summary>
		/// Gets a value indicating whether this instance is connected.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
		/// </value>
		bool IsConnected { get; }

		/// <summary>
		/// Gets the chanels.
		/// </summary>
		/// <value>
		/// The chanels.
		/// Definition and current value.
		/// </value>
		IList<ChanelData> Chanels { get; }

		/// <summary>
		/// Gets or sets the type of the module.
		/// </summary>
		/// <value>
		/// The type of the module.
		/// </value>
		string ModuleType { get; set; }
		#endregion

		#region Methode
		/// <summary>
		/// Connect the module.
		/// </summary>
		void Start();

		/// <summary>
		/// Closings module connection.
		/// </summary>
		void Closing();

		/// <summary>
		/// Reads the data from the module IO.
		/// Place the response value to the Channels list.
		/// </summary>
		void ReadData();

		/// <summary>
		/// Write new info to the chanel list and change the value to the ADAM module
		/// </summary>
		/// <param name="chanelId">Chanel Id.</param>
		/// <param name="digitalValue">Digital value (true/false).</param>
		/// <param name="anamlogValue">Analog value.</param>
		void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null);
		#endregion
	}
}