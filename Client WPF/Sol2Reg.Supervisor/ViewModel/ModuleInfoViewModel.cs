// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Supervisor\CheckModuleConnectionViewModel.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Supervisor\CheckModuleConnectionViewModel.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Supervisor\CheckModuleConnectionViewModel.cs
//     Created            : 05.01.2013 23:57
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Supervisor.ViewModel
{
	using System;
	using System.Collections.ObjectModel;
	using GalaSoft.MvvmLight.Command;
	using Sol2Reg.Supervisor.Model;
	using Sol2Reg.Supervisor.Utility;

	public class ModuleInfoViewModel : S2RViewModelBase<ModuleInfoViewModel>
	{
		private ObservableCollection<ModuleInfoModel> modules;
		private int forSimulation;

		public ModuleInfoViewModel()
		{
			this.GotoDetailCommand = new RelayCommand(this.DisplayDetail);
		}

		public RelayCommand GotoDetailCommand { get; private set; }

		/// <summary>Gets or sets the Pageable Collection for the Cattle of Owner.</summary>
		public ObservableCollection<ModuleInfoModel> Modules
		{
			get { return this.modules; }

			set
			{
				this.modules = value;
				this.RaisePropertyChanged(foo=> foo.Modules);
			}
		}

		public int ForSimulation
		{
			get { return this.forSimulation; }
			set { this.forSimulation = value; 
			this.RaisePropertyChanged(foo=>foo.forSimulation);}
		}

		/// <summary>Displays the detail.</summary>
		/// <exception cref="System.NotImplementedException" ></exception>
		private void DisplayDetail()
		{
			throw new NotImplementedException();
		}
	}
}