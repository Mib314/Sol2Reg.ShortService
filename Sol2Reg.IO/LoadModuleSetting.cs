// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO\LoadModuleSetting.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO\LoadModuleSetting.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO\LoadModuleSetting.cs
//     Created            : 28.12.2012 04:13
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace ModuleIO
{
	using System;
	using System.ComponentModel.Composition;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;
	using ModuleIO.Interface;
	using Sol2Reg.ServiceData;
	using Sol2Reg.ServiceData.Enumerations;

	/// <summary>Load setting and initialise Module IO.</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class LoadModuleSetting
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
		private const string chanel_ValueType = "TypeOfValue";
		private const string chanel_Description = "Description";
		private const string chanel_Comment = "Comment";

		private const string chanel_Gain = "Gain";
		private const string chanel_Offset = "Offset";
		#endregion

		[Import]
		private GlobalVariables GlobalVariables { get; set; }

		public IModules Modules { get; set; }

		public IModules LoadConfig(Func<string, string, IModuleBase> initialiseModule)
		{
			var doc = this.ReadFile();

			var modules = new Modules();

			foreach (var xModule in doc.Elements(tag_Module))
			{
				var moduleSerie = this.ReadAttribute(module_ModuleSerie, xModule);
				var moduleType = this.ReadAttribute(module_ModuleType, xModule);

				var module = initialiseModule(moduleSerie, moduleType);
				module.Name = this.ReadAttribute(module_Name, xModule);
				module.IpAddress = this.ReadAttribute(module_IP, xModule);
				var i = 0;
				if (ConvertToInt(this.ReadAttribute(module_Port, xModule), ref i)) module.Port = i;

				// Load Chanel for this module.
				foreach (var xChanel in xModule.Elements(tag_Chanel))
				{
					var id = -1;
					ConvertToInt(this.ReadAttribute(chanel_Id, xChanel), ref id);
					IChanel chanel;
					try
					{
						chanel = new Chanel(this.ReadAttribute(chanel_Id, xChanel, -1), this.ReadAttribute(chanel_Key, xChanel), (Direction)Enum.Parse(typeof(Direction), this.ReadAttribute(chanel_Direction, xChanel)), (TypeOfValue)Enum.Parse(typeof(TypeOfValue), this.ReadAttribute(chanel_ValueType, xChanel)));
					}
					catch (Exception)
					{
						throw new ArgumentNullException(string.Format("Not valide requiered argument to create Chanel id {0} in module name {1}", this.ReadAttribute(chanel_Id, xChanel, -1), module.Name));
					}
					chanel.Gain = this.ReadAttribute(chanel_Gain, xChanel, 1);
					chanel.Offset = this.ReadAttribute(chanel_Offset, xChanel, 0);
					chanel.Description = this.ReadAttribute(chanel_Description, xChanel);
					chanel.Comment = this.ReadAttribute(chanel_Comment, xChanel);

					module.Chanels.Add(chanel);
				}
				modules.Add(module);
			}
			return this.Modules;
		}

		/// <summary>Reads the config file.</summary>
		/// <returns>a XDocument content on the config modules file</returns>
		/// <exception cref="System.IO.IOException" >If the config file for modules not exist.</exception>
		private XDocument ReadFile()
		{
			// Assemble File path
			var filePath = !string.IsNullOrWhiteSpace(this.GlobalVariables.ConfigFilePath) ? string.Format("{0}\\{1}", this.GlobalVariables.ConfigFilePath, this.GlobalVariables.ModuleConfigName) : this.GlobalVariables.ModuleConfigName;
			// Check if the file exist
			if (!File.Exists(filePath)) throw new IOException(string.Format("The config file for module '{0}' don't exist.\nCheck the path and the file name.\nThis info is saved on the app.config section <AppConfig> file with the key {1} and {2}", filePath, GlobalVariables.ConfigFilePath_Key, GlobalVariables.ModuleConfigName_Key));

			var doc = XDocument.Load(filePath);
			return doc;
		}


		/// <summary>Reads the attribute.</summary>
		/// <param name="attributName" >Name of the attribut.</param>
		/// <param name="element" >The element.</param>
		/// <param name="defaultValue" >The default value.</param>
		/// <returns>String value.</returns>
		private string ReadAttribute(string attributName, XElement element, string defaultValue = "")
		{
			if (!element.HasAttributes) return string.Empty;

			var attribut = element.Attributes().FirstOrDefault(foo => foo.Name == attributName);
			if (attribut == null) return defaultValue;

			return attribut.Value;
		}

		/// <summary>Reads the attribute.</summary>
		/// <param name="attributName" >Name of the attribut.</param>
		/// <param name="element" >The element.</param>
		/// <param name="defaultValue" >The default value.</param>
		/// <returns>Integer value</returns>
		private int ReadAttribute(string attributName, XElement element, int defaultValue)
		{
			var i = 0;
			if (ConvertToInt(this.ReadAttribute(attributName, element, string.Empty), ref i)) return i;
			return defaultValue;
		}

		/// <summary>Converts to int.</summary>
		/// <param name="value" >The value.</param>
		/// <param name="valueToAssigne" >The value to assigne.</param>
		/// <returns>the integer value</returns>
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