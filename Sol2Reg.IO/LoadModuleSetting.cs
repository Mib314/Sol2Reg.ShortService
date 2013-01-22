// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO\LoadModuleSetting.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO\LoadModuleSetting.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO\LoadModuleSetting.cs
//     Created            : 28.12.2012 04:13
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Xml.Linq;
	using Sol2Reg.IO.Interface;
	using Sol2Reg.ServiceData;
	using Sol2Reg.ServiceData.Enumerations;
	using Sol2Reg.Tools;
	using Sol2Reg.Tools.Error;

	/// <summary>Load setting and initialise Module IO.</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class LoadModuleSetting
	{
		#region Tag et properties du xml de config
		public const string Tag_Modules = "Modules";
		public const string Tag_Module = "Module";
		public const string Tag_Chanel = "Chanel";
		public const string Module_Name = "Name";
		public const string Module_IP = "IP";
		public const string Module_Port = "Port";
		public const string Module_ModuleSerie = "ModuleSerie";
		public const string Module_ModuleType = "ModuleType";
		public const string Chanel_Id = "Id";
		public const string Chanel_Key = "Key";
		public const string Chanel_Direction = "Direction";
		public const string Chanel_TypeOfValue = "TypeOfValue";
		public const string Chanel_Description = "Description";
		public const string Chanel_Comment = "Comment";

		public const string Chanel_Gain = "Gain";
		public const string Chanel_Offset = "Offset";
		#endregion

		private string configFileFullName;

		[Import]
		public IFileSystem FileSystem { get; set; }

		[Import]
		public IGlobalVariables GlobalVar { get; set; }

		[Import]
		public ErrorTracking ErrorTracking { get; set; }

		[Import]
		public XmlLinq XmlLinq { get; set; }

		[Import]
		public ModuleDataValidator ModuleDataValidator { get; set; }

		[Import]
		public IModules Modules { get; set; }

		public IModules LoadConfig(Func<string, string, IModuleBase> initialiseModule)
		{
			var doc = this.ReadFile();
			if (doc == null)
			{
				return null;
			}

			var xModules = doc.Elements(Tag_Modules).FirstOrDefault();

			if (xModules == null)
			{
				this.ErrorTracking.Add(ErrorIdList.ConfigModuleIO_NoTagModules, ErrorGravity.FatalApplication, new[] {this.configFileFullName});
				return null;
			}

			if (!xModules.Elements(Tag_Module).Any())
			{
				this.ErrorTracking.Add(ErrorIdList.ConfigModuleIO_NoTagModule, ErrorGravity.FatalApplication, new[] {this.configFileFullName});
				return null;
			}

			foreach (var xModule in xModules.Elements(Tag_Module))
			{
				var moduleSerie = this.XmlLinq.ReadAttribute(Module_ModuleSerie, xModule);
				var moduleType = this.XmlLinq.ReadAttribute(Module_ModuleType, xModule);

				var module = initialiseModule(moduleSerie, moduleType);
				module.Name = this.XmlLinq.ReadAttribute(Module_Name, xModule);
				module.IpAddress = this.XmlLinq.ReadAttribute(Module_IP, xModule);
				module.Port = this.XmlLinq.ReadAttribute(Module_Port, xModule, GlobalVariables.DefaultPort);

				module.Chanels = new List<IChanel>();

				// Load Chanel for this module.
				foreach (var xChanel in xModule.Elements(Tag_Chanel))
				{
					IChanel chanel;
					try
					{
						chanel = new Chanel(this.XmlLinq.ReadAttribute(Chanel_Id, xChanel, -1), this.XmlLinq.ReadAttribute(Chanel_Key, xChanel), (Direction) Enum.Parse(typeof (Direction), this.XmlLinq.ReadAttribute(Chanel_Direction, xChanel)), (TypeOfValue) Enum.Parse(typeof (TypeOfValue), this.XmlLinq.ReadAttribute(Chanel_TypeOfValue, xChanel)));
					}
					catch (Exception exception)
					{
						this.ErrorTracking.Add(ErrorIdList.ConfigModuleIO_ReadChanel, ErrorGravity.FatalApplication, exception, new[] {this.configFileFullName, module.Name});
						return null;
					}
					chanel.Gain = this.XmlLinq.ReadAttribute(Chanel_Gain, xChanel, (float) 1);
					chanel.Offset = this.XmlLinq.ReadAttribute(Chanel_Offset, xChanel, (float) 0);
					chanel.Description = this.XmlLinq.ReadAttribute(Chanel_Description, xChanel);
					chanel.Comment = this.XmlLinq.ReadAttribute(Chanel_Comment, xChanel);

					module.Chanels.Add(chanel);
				}

				if (!this.ModuleDataValidator.ValidatData(module))
				{
					this.ErrorTracking.Add(ErrorIdList.ConfigModuleIO_ModuleDataNotValid, ErrorGravity.FatalApplication, new[] {module.Name});
					return null;
				}

				this.Modules.ModuleList.Add(module);
			}

			return this.Modules;
		}

		/// <summary>Reads the config file.</summary>
		/// <returns>a XDocument content on the config modules file</returns>
		/// <exception cref="System.IO.IOException" >If the config file for modules not exist.</exception>
		private XDocument ReadFile()
		{
			// Assemble File path
			this.configFileFullName = !string.IsNullOrWhiteSpace(this.GlobalVar.ConfigFilePath) ? string.Format("{0}\\{1}", this.GlobalVar.ConfigFilePath, this.GlobalVar.ModuleConfigName) : this.GlobalVar.ModuleConfigName;

			// Check if the file exist
			if (!this.FileSystem.FileExists(this.configFileFullName))
			{
				this.configFileFullName = string.Format("{0}{1}", this.FileSystem.GetDLLPathForThisClass<LoadModuleSetting>(), this.GlobalVar.ModuleConfigName);
			}

			// Check if the file exist
			if (!this.FileSystem.FileExists(this.configFileFullName))
			{
				this.ErrorTracking.Add(ErrorIdList.ConfigModuleIO_NoFile, ErrorGravity.FatalApplication, new[] {this.configFileFullName, GlobalVariables.ConfigFilePath_Key, GlobalVariables.ModuleConfigName_Key});
				return null;
			}

			try
			{
				return this.FileSystem.LoadXml(this.configFileFullName);
			}
			catch (Exception exception)
			{
				this.ErrorTracking.Add(ErrorIdList.ConfigModuleIO_FileBadFormated, ErrorGravity.FatalApplication, exception, new[] {this.configFileFullName});

				return null;
			}
		}
	}
}