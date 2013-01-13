using System.Windows;

namespace Sol2Reg.Supervisor
{

	/// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public App()
		{
			this.Startup += this.HandleApplicationStartup;

			InitializeComponent();
		}

	    private void HandleApplicationStartup(object sender, StartupEventArgs e)
	    {
		    //Application.Current.Resources["Locator"] = new Locator();
		    //var bootstrapper = new BootStrapper(p_StartupEventArgs);
		    //bootstrapper.Run();

		    //// The HandleUnhandledException event get registered in TVKplus.Client.Startup.Module
		    //// With this deregistration a double handled exception is prevented
		    //var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
		    //eventAggregator.GetEvent<Deregister404OnShellEvent>().Subscribe((p_Args) => this.UnhandledException -= HandleUnhandledException, true);
	    }
    }
}
