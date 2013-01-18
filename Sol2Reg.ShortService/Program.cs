// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ShortService\Program.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ShortService\Program.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ShortService\Program.cs
//     Created            : 17.12.2012 16:31
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ShortService
{
	using System.ServiceProcess;

	internal static class Program
	{
		/// <summary>Point d'entrée principal de l'application.</summary>
		/// <param name="args" >The args : C : Console mode. S : Simulation mode.</param>
		private static void Main(string[] args)
		{
			var startConsole = false;
			var startSimulator = false;

			if (args.Length > 0)
			{
				foreach (var arg in args)
				{
					if (arg.ToLower() == "c") startConsole = true;
					else if (arg.ToLower() == "s") startSimulator = true;
				}
			}

			if (startConsole)
			{
				var consolAppStart = new ConsoleAppStart();
				consolAppStart.Run(startSimulator);

			}

			// start in service mode
			var servicesToRun = new ServiceBase[]
								{
									new ShortServices(startSimulator)
								};
			ServiceBase.Run(servicesToRun);
		}
	}
}