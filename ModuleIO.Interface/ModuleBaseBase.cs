﻿// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO.Interface\Module.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO.Interface\Module.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO.Interface\Module.cs
//     Created            : 28.12.2012 22:07
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ModuleIO.Interface
{
	using System;
	using System.Collections.Generic;
	using ModuleIO.Interface.Enumerations;

	/// <summary>
	/// Base module for ineritence.
	/// </summary>
	public abstract class ModuleBaseBase : IModuleBase
	{
		protected ModuleBaseBase()
		{
			this.Chanels = new List<IChanelData>();
			this.Errors = new ModuleErrors();
		}

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
		public IList<IChanelData> Chanels { get; private set; }

		/// <summary>
		/// Gets the errors list.
		/// </summary>
		/// <value>
		/// The errors.
		/// </value>
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

		/// <summary>Gets a value indicating whether this instance is connected.</summary>
		/// <value>
		///     <c>true</c> if this instance is connected; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsConnected { get; protected set; }
		#endregion

		#region Abstract

		/// <summary>Initialyses the module.</summary>
		public abstract void InitialyseModule();

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

		/// <summary>Adds to module error a error module.</summary>
		/// <param name="errorCode" >The error code.</param>
		protected void AddModuleError(ModuleErrorCode errorCode)
		{
			this.Errors.Add(this.Name, this.IpAddress, this.Port, errorCode, DateTime.Now);
		}

		/// <summary>Adds to module error a error module.</summary>
		/// <param name="chanelData" >The chanel data.</param>
		/// <param name="errorCode" >The error code.</param>
		protected void AddModuleError(IChanelData chanelData, ModuleErrorCode errorCode)
		{
			this.Errors.Add(this.Name, this.IpAddress, this.Port, chanelData.Id, chanelData.Key, errorCode, DateTime.Now);
		}
	}
}