
namespace Sol2Reg.ShortService
{
	using System.ComponentModel.Composition;
	using Sol2Reg.Service;
	using Sol2Reg.ServiceData;

	internal class ConsoleAppStart
	{
		[Import(typeof(ISol2RegService))]
		private ISol2RegService Sol2RegService { get; set; }

		[Import]
		private GlobalVariables globalVariables;

		public ConsoleAppStart()
		{
			new BootStrap(this);
		}

		internal void Run(bool startSimulator)
		{
			this.globalVariables.Initialize();
			this.Sol2RegService.Initilize(startSimulator);
		}
	}
}
