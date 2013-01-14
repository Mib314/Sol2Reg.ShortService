// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO.Interface\IModuleBase.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO.Interface\IModuleBase.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO.Interface\IModuleBase.cs
//     Created            : 28.12.2012 04:20
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ModuleIO.Interface
{
	using System.Collections.Generic;
	using Sol2Reg.ServiceData;

	/// <summary>
	/// Interface to define ModuleIO
	/// </summary>
	public interface IModuleBase
	{
		#region Properties
		/// <summary>Gets or sets the name.</summary>
		/// <value>The name.</value>
		string Name { get; set; }

		/// <summary>Gets the module serie.</summary>
		/// <value>The module serie.</value>
		string ModuleSerie { get; }

		/// <summary>Gets the type of the module.</summary>
		/// <value>The type of the module.</value>
		string ModuleType { get; }

		/// <summary>Gets or sets the ip address.</summary>
		/// <value>The ip address.</value>
		string IpAddress { get; set; }

		/// <summary>Gets or sets the port.</summary>
		/// <value>The port.</value>
		int Port { get; set; }

		/// <summary>
		/// Gets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		string Password { get; }

		/// <summary>Gets a value indicating whether this instance is connected.</summary>
		/// <value>
		///     <c>true</c> if this instance is connected; otherwise, <c>false</c>.
		/// </value>
		bool IsConnected { get; }

		/// <summary>Gets a value indicating whether this instance is initialized.</summary>
		/// <value>
		///     <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
		/// </value>
		bool IsInitialized { get; }

		/// <summary>Gets the chanels.</summary>
		/// <value>The chanels. Definition and current value.</value>
		IList<IChanel> Chanels { get; }

		/// <summary>Gets the modules parent.</summary>
		/// <value>The modules.</value>
		IModules Modules { get; }

		/// <summary>Gets the errors list.</summary>
		/// <value>The errors.</value>
		IModuleErrors Errors { get; }
		#endregion

		#region Methode
		/// <summary>Initialyses the module.</summary>
		void InitialyseModule();

		/// <summary>Connect the module.</summary>
		void Start();

		/// <summary>Closings module connection.</summary>
		void Closing();

		/// <summary>Reads the data from the module IO. Place the response value to the Channels list.</summary>
		void ReadData();

		/// <summary>Write new info to the chanel list and change the value to the ADAM module</summary>
		/// <param name="chanelId" >Chanel Id.</param>
		/// <param name="digitalValue" >Digital value (true/false).</param>
		/// <param name="anamlogValue" >Analog value.</param>
		void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null);
		#endregion
	}
}