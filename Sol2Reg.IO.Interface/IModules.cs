// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO.Interface\IModules.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO.Interface\IModules.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO.Interface\IModules.cs
//     Created            : 28.12.2012 04:38
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Interface
{
	using System.Collections.Generic;
	using System.ComponentModel.Composition;

	/// <summary>
	/// Modules liste.
	/// </summary>
	[InheritedExport]
	public interface IModules : IList<IModuleBase>
	{
		/// <summary>
		/// Gets or sets a value indicating whether [simulation mode].
		/// </summary>
		/// <value>
		///   <c>true</c> if [simulation mode]; otherwise, <c>false</c>.
		/// </value>
		bool IsSimulationMode { get; }

		/// <summary>Starts all modules connection.</summary>
		void Start();

		/// <summary>Closes all modules connection.</summary>
		void Closing();


		/// <summary>Reads the data.</summary>
		void ReadData();

		/// <summary>Adds the new module.</summary>
		/// <param name="name" >The name.</param>
		/// <param name="moduleType" >Type of the module.</param>
		/// <param name="moduleSerie" >The module serie.</param>
		/// <param name="ipAddress" >The ip address.</param>
		/// <param name="port" >The port.</param>
		/// <param name="chanels" >The chanels.</param>
		void AddNewModule(string name, string moduleType, string moduleSerie, string ipAddress, int port, List<IChanel> chanels);

		/// <summary>
		/// Initializes the specified is simulation mode.
		/// </summary>
		/// <param name="isSimulationMode">if set to <c>true</c> [is simulation mode].</param>
		/// <returns>Modules list.</returns>
		IModules Initialize(bool isSimulationMode = false);
	}
}