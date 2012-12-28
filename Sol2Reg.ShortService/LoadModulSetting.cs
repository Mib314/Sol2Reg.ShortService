// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ShortService\LoadModulSetting.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ShortService\LoadModulSetting.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ShortService\LoadModulSetting.cs
//     Created            : 28.12.2012 00:13
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ShortService
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;
	using ADAM6000Com;
	using ModuleIO;
	using ValueType = ModuleIO.ValueType;

	public class LoadModulSetting
	{
		#region Tag et properties du xml de config
		private const string tag_Modules = "Modules";
		private const string tag_Module = "Module";
		private const string tag_Chanel = "Chanel";
		private const string module_Name = "Name";
		private const string module_IP = "IP";
		private const string module_Port = "Port";
		private const string module_ModuleSerie = "ModuleSerie";
		private const string module_ModuleType = "ModuleType";
		private const string chanel_Id = "Id";
		private const string chanel_Key = "Key";
		private const string chanel_Direction = "Direction";
		private const string chanel_ValueType = "ValueType";
		private const string chanel_Description = "Description";
		private const string chanel_Comment = "Comment";

		private const string chanel_Gain = "Gain";
		private const string chanel_Offset = "Offset";
		#endregion

		public List<IModuleIO> Modules { get; set; }

		public void LoadConfig()
		{
			var doc = this.ReadFile();

			foreach (var xModule in doc.Elements(tag_Module))
			{
				var moduleSerie = this.ReadAttribute(module_ModuleSerie, xModule);
				var moduleType = this.ReadAttribute(module_ModuleType, xModule);

				var module = this.InitialiseModule(moduleSerie, moduleType);

				module.Name = this.ReadAttribute(module_Name, xModule);
				module.IpAddress = this.ReadAttribute(module_IP, xModule);
				var i = 0;
				if (ConvertToInt(this.ReadAttribute(module_Port, xModule), ref i)) module.Port = i;

				// Load Chanel for this module.
				foreach (var xChanel in xModule.Elements(tag_Chanel))
				{
					var id = -1;
					ConvertToInt(this.ReadAttribute(chanel_Id, xChanel), ref id);
					ChanelData chanel;
					try
					{
						chanel = new ChanelData(this.ReadAttribute(chanel_Id, xChanel, -1), this.ReadAttribute(chanel_Key, xChanel), (Direction) Enum.Parse(typeof (Direction), this.ReadAttribute(chanel_Direction, xChanel)), (ValueType) Enum.Parse(typeof (ValueType), this.ReadAttribute(chanel_ValueType, xChanel)));
					}
					catch (Exception)
					{
						throw new ArgumentNullException(string.Format("Not valide requiered argument to create Chanel id {0} in module name {1}", this.ReadAttribute(chanel_Id, xChanel, -1), module.Name));
					}
					chanel.Gain = this.ReadAttribute(chanel_Gain, xChanel, 1);
					chanel.Offset = this.ReadAttribute(chanel_Offset, xChanel, 0);
					chanel.Description = this.ReadAttribute(chanel_Description, xChanel);
					chanel.Comment = this.ReadAttribute(chanel_Comment, xChanel);
				}
			}
		}

		/// <summary>Reads the config file.</summary>
		/// <returns>a XDocument content on the config modules file</returns>
		/// <exception cref="System.IO.IOException" >If the config file for modules not exist.</exception>
		private XDocument ReadFile()
		{
			// Assemble File path
			var filePath = !string.IsNullOrWhiteSpace(GlobalVariables.ConfigFilePath) ? string.Format("{0}\\{1}", GlobalVariables.ConfigFilePath, GlobalVariables.ModuleConfigName) : GlobalVariables.ModuleConfigName;
			// Check if the file exist
			if (!File.Exists(filePath)) throw new IOException(string.Format("The config file for module '{0}' don't exist.\nCheck the path and the file name.\nThis info is saved on the app.config section <AppConfig> file with the key {1} and {2}", filePath, GlobalVariables.ConfigFilePath_Key, GlobalVariables.ModuleConfigName_Key));

			var doc = XDocument.Load(filePath);
			return doc;
		}

		private IModuleIO InitialiseModule(string moduleSerie, string moduleType)
		{
			switch (moduleSerie)
			{
				case "Adam6000Type":
				{
					switch (moduleType)
					{
						case "Adam6015":
							return new Adam6015();
						case "Adam6066":
							return new Adam6066();
						default:
							throw new ArgumentOutOfRangeException(moduleType);
					}
				}
				default:
				{
					throw new ArgumentOutOfRangeException(moduleSerie);
				}
			}
		}

		private string ReadAttribute(string attributName, XElement element, string defaultValue = "")
		{
			if (!element.HasAttributes) return string.Empty;

			var attribut = element.Attributes().FirstOrDefault(foo => foo.Name == attributName);
			if (attribut == null) return defaultValue;

			return attribut.Value;
		}

		private int ReadAttribute(string attributName, XElement element, int defaultValue)
		{
			var i = 0;
			if (ConvertToInt(this.ReadAttribute(attributName, element, string.Empty), ref i)) return i;
			return defaultValue;
		}

		private static bool ConvertToInt(string value, ref int valueToAssigne)
		{
			int tempValue;
			if (int.TryParse(value, out tempValue))
			{
				valueToAssigne = tempValue;
				return true;
			}
			return false;
		}
	}
}