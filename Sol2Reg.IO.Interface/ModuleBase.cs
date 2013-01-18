// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Interface\ModuleBase.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Interface\ModuleBase.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Interface\ModuleBase.cs
//     Created            : 28.12.2012 23:07
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ModuleIO.Interface
{
	using System;
	using System.Collections.Generic;
	using Sol2Reg.ServiceData;
	using Sol2Reg.ServiceData.Enumerations;

	/// <summary>Base module for ineritence.</summary>
	public abstract class ModuleBase : IModuleBase
	{
		#region Property
		/// <summary>Gets or sets the name.</summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>Gets or sets the ip address.</summary>
		/// <value>The ip address.</value>
		public string IpAddress { get; set; }

		/// <summary>Gets or sets the port.</summary>
		/// <value>The port.</value>
		public int Port { get; set; }

		/// <summary>Gets the chanels.</summary>
		/// <value>The chanels. Definition and current value.</value>
		public IList<IChanel> Chanels { get; set; }

		/// <summary>Gets the modules parent.</summary>
		/// <value>The modules.</value>
		public IModules Modules { get; private set; }

		/// <summary>Gets the errors list.</summary>
		/// <value>The errors.</value>
		public IModuleErrors Errors { get; private set; }

		/// <summary>Gets a value indicating whether this instance is initialized.</summary>
		/// <value>
		///     <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
		/// </value>
		public bool IsInitialized { get; protected set; }
		#endregion

		#region Virtual property
		/// <summary>Gets the module serie.</summary>
		/// <value>The module serie.</value>
		public virtual string ModuleSerie { get; protected set; }

		/// <summary>Gets the type of the module.</summary>
		/// <value>The type of the module.</value>
		public virtual string ModuleType { get; protected set; }

		/// <summary>Gets the password.</summary>
		/// <value>The password.</value>
		public string Password { get; protected set; }

		/// <summary>Gets a value indicating whether this instance is connected.</summary>
		/// <value>
		///     <c>true</c> if this instance is connected; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsConnected { get; protected set; }
		#endregion

		#region Abstract

		/// <summary>Initialize the module.</summary>
		public abstract IModuleBase Initialize(string moduleSerie, string moduleType, IModules modules);

		/// <summary>Connect the module.</summary>
		public abstract void Start();

		/// <summary>Closings module connection.</summary>
		public abstract void Closing();

		/// <summary>Reads the data from the module IO. Place the response value to the Channels list.</summary>
		public abstract void ReadData();

		/// <summary>Write new info to the chanel list and change the value to the ADAM module</summary>
		/// <param name="chanelId" >Chanel Id.</param>
		/// <param name="digitalValue" >Digital value (true/false).</param>
		/// <param name="anamlogValue" >Analog value.</param>
		public abstract void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null);
		#endregion

		/// <summary>Initializes the specified modules.</summary>
		/// <param name="modules" >The modules.</param>
		/// <returns>The module base interface.</returns>
		protected IModuleBase InitializeBase(IModules modules)
		{
			this.Modules = modules;
			this.Chanels = new List<IChanel>();
			this.Errors = new ModuleErrors();
			return this;
		}

		/// <summary>Adds to module error a error module.</summary>
		/// <param name="errorCode" >The error code.</param>
		protected void AddModuleError(ModuleErrorCode errorCode)
		{
			this.Errors.Add(this.Name, this.IpAddress, this.Port, errorCode, DateTime.Now);
		}

		/// <summary>Adds to module error a error module.</summary>
		/// <param name="chanel" >The chanel data.</param>
		/// <param name="errorCode" >The error code.</param>
		protected void AddModuleError(IChanel chanel, ModuleErrorCode errorCode)
		{
			this.Errors.Add(this.Name, this.IpAddress, this.Port, chanel.Id, chanel.Key, errorCode, DateTime.Now);
		}
	}
}