using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Sol2Reg.ShortService
{
	static class Program
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		static void Main()
		{
			var servicesToRun = new ServiceBase[] 
				{ 
					new ShortServices() 
				};
			ServiceBase.Run(servicesToRun);
		}
	}
}
