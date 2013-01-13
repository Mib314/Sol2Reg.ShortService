// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.GuiModule.Base\App.xaml.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.GuiModule.Base\App.xaml.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.GuiModule.Base\App.xaml.cs
//     Created            : 12.01.2013 16:25
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.GuiModule.Base
{
	using System;
	using System.Diagnostics;
	using System.Windows;
	using System.Windows.Browser;

	public partial class App
	{
		public App()
		{
			this.Startup += this.Application_Startup;
			this.Exit += this.Application_Exit;
			this.UnhandledException += this.Application_UnhandledException;

			this.InitializeComponent();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			this.RootVisual = new MainPage();
		}

		private void Application_Exit(object sender, EventArgs e) {}

		private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			// Si l'application s'exécute en dehors du débogueur, signalez l'exception à l'aide du
			// mécanisme d'exception du navigateur. Sur IE une icône verte s'affichera 
			// dans la barre d'état et dans Firefox un script d'erreur s'affichera.
			if (!Debugger.IsAttached)
			{
				// REMARQUE : cela permet à l'application de continuer à s'exécuter après qu'une exception a été levée
				// mais pas gérée. 
				// Pour des applications de production, cette gestion des erreurs doit être remplacée par un système qui 
				// signale l'erreur au site Web et arrête l'application.
				e.Handled = true;
				Deployment.Current.Dispatcher.BeginInvoke(delegate { this.ReportErrorToDOM(e); });
			}
		}

		private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
		{
			try
			{
				var errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
				errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

				HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
			}
			catch (Exception) {}
		}
	}
}