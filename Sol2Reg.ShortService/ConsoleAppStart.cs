
namespace Sol2Reg.ShortService
{
	using System.ComponentModel.Composition;
	using Sol2Reg.Service;

	internal class ConsoleAppStart
	{
		[Import(typeof(ISol2RegService))]
		private ISol2RegService Sol2RegService { get; set; }

		internal void Run(bool startSimulator)
		{
			this.Sol2RegService.Initilize(startSimulator);
		}
	}
}
