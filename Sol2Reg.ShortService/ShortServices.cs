// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ShortService\ShortServices.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ShortService\ShortServices.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ShortService\ShortServices.cs
//     Created            : 17.12.2012 16:35
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ShortService
{
	using System.ServiceProcess;

	partial class ShortServices : ServiceBase
	{
		public ShortServices()
		{
			this.InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			// TODO: ajoutez ici le code pour démarrer votre service.
		}

		protected override void OnStop()
		{
			// TODO: ajoutez ici le code pour effectuer les destructions nécessaires à l'arrêt de votre service.
		}
	}
}