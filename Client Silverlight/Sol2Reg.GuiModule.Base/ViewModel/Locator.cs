// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Supervisor\Class1.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Supervisor\Class1.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Supervisor\Class1.cs
//     Created            : 07.01.2013 00:46
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Supervisor.ViewModel
{
	using System.Windows;
	using Sol2Reg.GuiModule.Base.Model;
	using TVKplus.Client.Infrastructure.DependencyInjection;

	/// <summary>
	/// This class contains static references to all the view models in the application and provides an entry point for the bindings.
	/// <para>
	/// Use the <strong>Mvvmlocatorproperty</strong> snippet to add ViewModels to this locator.
	/// </para>
	/// <para>In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:.</para>
	/// <code>
	/// &lt;Application.Resources&gt;
	/// &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:MvvmLight1.ViewModel"
	/// x:Key="Locator" /&gt;
	/// &lt;/Application.Resources&gt;
	/// </code>
	/// <para>Then use:.</para>
	/// <code>
	/// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
	/// </code>
	/// <para>You can also use Blend to do all this with the tool's support.</para>
	/// <para>See http://www.galasoft.ch/mvvm/getstarted.</para>
	/// <para>
	/// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete the Main property and bind to the ViewModelNameStatic property instead:.
	/// </para>
	/// <code>
	/// xmlns:vm="clr-namespace:MvvmLight1.ViewModel"
	/// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
	/// </code>
	/// .
	/// </summary>
	public class Locator
	{
		/// <summary>Gets the shared Locator instance from the App's resources collection.</summary>
		public static Locator Instance
		{
			get { return Application.Current.Resources["Locator"] as Locator; }
		}

		/// <summary>
		/// Gets the module info model.
		/// </summary>
		/// <value>
		/// The module info model.
		/// </value>
		public ModuleInfoModel ModuleInfoModel
		{
			get { return SystemConfiguration.Resolver.Resolve<ModuleInfoModel>(); }
		}

		/// <summary>
		/// Gets the chanel info model.
		/// </summary>
		/// <value>
		/// The chanel info model.
		/// </value>
		public ChanelInfoModel ChanelInfoModel
		{
			get { return SystemConfiguration.Resolver.Resolve<ChanelInfoModel>(); }
		}

		/// <summary>
		/// Gets the module info view model.
		/// </summary>
		/// <value>
		/// The module info view model.
		/// </value>
		public ModuleInfoViewModel ModuleInfoViewModel
		{
			get { return SystemConfiguration.Resolver.Resolve<ModuleInfoViewModel>(); }
		}
	}
}