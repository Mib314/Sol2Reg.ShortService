// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ShortService\BootStrap.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ShortService\BootStrap.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ShortService\BootStrap.cs
//     Created            : 16.01.2013 01:55
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ShortService
{
	using System;
	using System.ComponentModel.Composition;
	using System.ComponentModel.Composition.Hosting;
	using System.IO;

	internal class BootStrap
	{
		private static DirectoryCatalog s_Catalog;
		private static CompositionContainer s_CompositionContainer;

		public BootStrap(object there)
		{
			try
			{
				var aggregatecatalogue = new AggregateCatalog();
				//aggregatecatalogue.Catalogs.Add(new AssemblyCatalog(
				//    Assembly.GetExecutingAssembly()));
				aggregatecatalogue.Catalogs.Add(new DirectoryCatalog(
													AppDomain.CurrentDomain.BaseDirectory));

				var container = new CompositionContainer(
					aggregatecatalogue);
				container.ComposeParts(there);
			}
			catch (FileNotFoundException fnfex)
			{
				Console.WriteLine(fnfex.Message);
			}
			catch (CompositionException cex)
			{
				Console.WriteLine(cex.Message);
			}
			//this.CreateCompositionContainerFromDirectory(there);
		}

		private void CreateCompositionContainerFromDirectory(object there)
		{
			s_Catalog = new DirectoryCatalog(@".\");
			s_CompositionContainer = new CompositionContainer(s_Catalog);
			s_CompositionContainer.ComposeParts(there);
		}
	}
}