// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Interface.Test\Implement_ModuleBase.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Interface.Test\Implement_ModuleBase.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Interface.Test\Implement_ModuleBase.cs
//     Created            : 20.01.2013 21:37
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Interface.Test
{
	using System;

	internal class Implement_ModuleBase : ModuleBase
	{
		/// <summary>Initialize the module.</summary>
		public override IModuleBase Initialize(string moduleSerie, string moduleType, IModules modules)
		{
			this.ModuleSerie = moduleSerie;
			this.ModuleType = moduleType;
			this.InitializeBase(modules);
			return this;
		}

		/// <summary>Connect the module.</summary>
		public override void Start()
		{
			throw new NotImplementedException();
		}

		/// <summary>Closings module connection.</summary>
		public override void Closing()
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads the data from the module IO. Place the response value to the Channels list.</summary>
		public override void ReadData()
		{
			throw new NotImplementedException();
		}

		/// <summary>Write new info to the chanel list and change the value to the ADAM module</summary>
		/// <param name="chanelId" >Chanel Id.</param>
		/// <param name="digitalValue" >Digital value (true/false).</param>
		/// <param name="anamlogValue" >Analog value.</param>
		public override void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null)
		{
			throw new NotImplementedException();
		}
	}
}