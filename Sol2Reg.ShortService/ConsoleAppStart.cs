// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ShortService\ConsoleAppStart.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ShortService\ConsoleAppStart.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ShortService\ConsoleAppStart.cs
//     Created            : 16.01.2013 07:27
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ShortService
{
	using System.ComponentModel.Composition;
	using Sol2Reg.Service;
	using Sol2Reg.ServiceData;

	internal class ConsoleAppStart
	{
		[Import]
		private GlobalVariables globalVariables;

		public ConsoleAppStart()
		{
			new BootStrap(this);
		}

		[Import(typeof (ISol2RegService))]
		private ISol2RegService Sol2RegService { get; set; }

		internal void Run(bool startSimulator)
		{
			this.globalVariables.Initialize();
			this.Sol2RegService.Initilize(startSimulator);
		}
	}
}