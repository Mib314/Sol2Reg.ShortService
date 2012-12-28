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
		private static void Main()
		{
			var servicesToRun = new ServiceBase[]
								{
									new ShortServices()
								};
			ServiceBase.Run(servicesToRun);
		}
	}
}