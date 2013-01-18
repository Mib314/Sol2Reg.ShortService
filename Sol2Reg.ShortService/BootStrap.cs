// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ShortService\ContainerConfiguration.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ShortService\ContainerConfiguration.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ShortService\ContainerConfiguration.cs
//     Created            : 16.01.2013 01:55
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ShortService
{
	using System.ComponentModel.Composition;
	using System.ComponentModel.Composition.Hosting;

	internal class BootStrap
	{
		private static DirectoryCatalog s_Catalog;
		private static CompositionContainer s_CompositionContainer;

		public BootStrap(object there)
		{
			this.CreateCompositionContainerFromDirectory(there);
		}

		private void CreateCompositionContainerFromDirectory(object there)
		{
			s_Catalog = new DirectoryCatalog(@".\");
			s_CompositionContainer = new CompositionContainer(s_Catalog);
			s_CompositionContainer.ComposeParts(there);
		}
	}
}