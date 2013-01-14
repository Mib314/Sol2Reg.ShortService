// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Supervisor\ModuleInfoModel.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Supervisor\ModuleInfoModel.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Supervisor\ModuleInfoModel.cs
//     Created            : 05.01.2013 23:57
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Supervisor.Model
{

	using Sol2Reg.Supervisor.Utility;

	public class ModuleInfoModel : S2RViewModelBase<ModuleInfoModel>
	{
		private string infoChanel;
		private string ipAddress_Port;
		private string moduleName;
		private string type_Serie;

		/// <summary>Gets or sets the name of the module.</summary>
		/// <value>The name of the module.</value>
		public string ModuleName
		{
			get
			{
				//if (IsInDesignMode) return "Module Name";
				return this.moduleName;
			}
			set
			{
				if (this.moduleName == value) return;
				this.moduleName = value;
				RaisePropertyChanged(foo=> foo.ModuleName);
			}
		}

		/// <summary>Gets or sets the type serie.</summary>
		/// <value>The type_ serie.</value>
		public string Type_Serie
		{
			get { return this.type_Serie; }
			set
			{
				if (this.type_Serie == value) return;
				this.type_Serie = value;
				RaisePropertyChanged(foo => foo.Type_Serie);
			}
		}

		/// <summary>Gets or sets the ip address port.</summary>
		/// <value>The ip address_ port.</value>
		public string IpAddress_Port
		{
			get { return this.ipAddress_Port; }
			set
			{
				if (this.ipAddress_Port == value) return;
				this.ipAddress_Port = value;
				RaisePropertyChanged(foo => foo.IpAddress_Port);
			}
		}

		/// <summary>Gets or sets the info chanel.</summary>
		/// <value>The info chanel.</value>
		public string InfoChanel
		{
			get { return this.infoChanel; }
			set
			{
				if (this.infoChanel == value) return;
				this.infoChanel = value;
				RaisePropertyChanged(foo => foo.InfoChanel);
			}
		}
	}
}