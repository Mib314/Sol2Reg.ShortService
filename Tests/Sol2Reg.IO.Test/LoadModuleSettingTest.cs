﻿// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Test\LoadModuleSettingTest.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Test\LoadModuleSettingTest.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Test\LoadModuleSettingTest.cs
//     Created            : 21.01.2013 09:34
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Test
{
	using System.Collections.Generic;
	using System.IO;
	using System.Xml.Linq;
	using Moq;
	using Sol2Reg.IO.Interface;
	using Sol2Reg.ServiceData;
	using Sol2Reg.ServiceData.Enumerations;
	using Sol2Reg.Tools;
	using Sol2Reg.Tools.Error;
	using Xunit;

	public class LoadModuleSettingTest
	{
		private const string PATH = "Path";
		private const string FILE = "FILETEST";
		private const string PATH_DLL = "PathDLL\\";

		private const string MODULE_NAME = "MesureTemp";
		private const string MODULE_SERIE = "Adam6000";
		private const string MODULE_TYPE = "Adam6015";
		private const string MODULE_IP = "192.168.200.150";
		private const int MODULE_PORT = 520;

		private const int CHANEL_ID = 1;
		private const string CHANEL_KEY = "KEY";
		private const Direction CHANEL_DIRECTION = Direction.Input;
		private const TypeOfValue CHANEL_TYPE_OF_VALUE = TypeOfValue.Analog;
		private const float CHANEL_GAIN = 1;
		private const float CHANEL_OFFSET = 0;
		private const string CHANEL_DESCRIPTION = "Chanel_Description";
		private const string CHANEL_COMMENT = "Chanel_Comment";

		private readonly Mock<ErrorTracking> errorTracking;


		private readonly Mock<IFileSystem> fileSystem;
		private readonly Mock<IGlobalVariables> globalVariables;
		private readonly Mock<ModuleDataValidator> moduleDataValidator;
		private readonly Mock<IModules> modules;

		private readonly LoadModuleSetting testee;
		private readonly XmlLinq xmlLinq;
		private FackInitializeModules fackInitializeModules;
		private IModuleBase moduleBase;
		private string pathDll_File;
		private string path_File;

		public LoadModuleSettingTest()
		{
			this.fileSystem = new Mock<IFileSystem>();
			this.globalVariables = new Mock<IGlobalVariables>();
			this.errorTracking = new Mock<ErrorTracking>();
			this.moduleDataValidator = new Mock<ModuleDataValidator>();
			this.modules = new Mock<IModules>();

			this.xmlLinq = new XmlLinq();

			this.testee = new LoadModuleSetting {FileSystem = this.fileSystem.Object, GlobalVar = this.globalVariables.Object, ErrorTracking = this.errorTracking.Object, XmlLinq = this.xmlLinq, ModuleDataValidator = this.moduleDataValidator.Object, Modules = this.modules.Object};

			this.SetupVariable();
		}


		[Fact]
		public void LoadConfigWhenModuleDataValidThenModules()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem();
			this.moduleDataValidator.Setup(foo => foo.ValidatData(It.IsAny<IModuleBase>())).Returns(true);

			var xDoc = new XDocument();
			var xModules = new XElement(LoadModuleSetting.Tag_Modules);
			xModules.Add(this.SetValidModule());
			xDoc.Add(xModules);

			this.fileSystem.Setup(foo => foo.LoadXml(this.path_File)).Returns(xDoc);

			var result = this.testee.LoadConfig(this.fackInitializeModules.InitializeModule);

			Assert.NotNull(result);
			this.CheckValidModule(result);
			this.CheckValidChanel(result);
		}

		[Fact]
		public void LoadConfigWhenNoFolderInfoThenModules()
		{
			this.SetupGlobalVariables(string.Empty, FILE);
			this.fileSystem.Setup(foo => foo.FileExists(FILE)).Returns(true);

			this.fileSystem.Setup(foo => foo.LoadXml(FILE)).Returns(new XDocument());
			this.fileSystem.Setup(foo => foo.GetDLLPathForThisClass<LoadModuleSetting>()).Returns(PATH_DLL);
			this.moduleDataValidator.Setup(foo => foo.ValidatData(It.IsAny<IModuleBase>())).Returns(true);

			var xDoc = new XDocument();
			var xModules = new XElement(LoadModuleSetting.Tag_Modules);
			xModules.Add(this.SetValidModule());
			xDoc.Add(xModules);

			this.fileSystem.Setup(foo => foo.LoadXml(FILE)).Returns(xDoc);

			//this.SetModulesWithValidModule();

			var result = this.testee.LoadConfig(this.fackInitializeModules.InitializeModule);

			Assert.NotNull(result);
			this.CheckValidModule(result);
			this.CheckValidChanel(result);
		}

		[Fact]
		public void LoadConfigWhenFileNotExistThenErrorNoFile()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem(false, false);
			var restult = this.testee.LoadConfig(null);

			this.CheckError(ErrorIdList.ConfigModuleIO_NoFile, ErrorGravity.FatalApplication);
			Assert.Null(restult);
		}

		[Fact]
		public void LoadConfigWhenFileNotWellForematedThenErrorFileBadFormated()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem();
			this.fileSystem.Setup(foo => foo.LoadXml(this.path_File)).Throws<InvalidDataException>();

			var restult = this.testee.LoadConfig(null);

			this.CheckError(ErrorIdList.ConfigModuleIO_FileBadFormated, ErrorGravity.FatalApplication);
			Assert.Null(restult);
		}

		[Fact]
		public void LoadConfigWhenTagModulesNotExistThenErrorNoTagModules()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem();
			var xDoc = new XDocument();
			var xModules = new XElement("Test");
			xDoc.Add(xModules);

			this.fileSystem.Setup(foo => foo.LoadXml(this.path_File)).Returns(xDoc);

			var restult = this.testee.LoadConfig(null);

			this.CheckError(ErrorIdList.ConfigModuleIO_NoTagModules, ErrorGravity.FatalApplication);
			Assert.Null(restult);
		}

		[Fact]
		public void LoadConfigWhenTagModuleNotExistThenErrorNoTagModule()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem();
			var xDoc = new XDocument();
			var xModules = new XElement(LoadModuleSetting.Tag_Modules);
			xDoc.Add(xModules);

			this.fileSystem.Setup(foo => foo.LoadXml(this.path_File)).Returns(xDoc);

			var restult = this.testee.LoadConfig(null);

			this.CheckError(ErrorIdList.ConfigModuleIO_NoTagModule, ErrorGravity.FatalApplication);
			Assert.Null(restult);
		}

		[Fact]
		public void LoadConfigWhenModuleDataNotValidThenErrorModuleDataNotValid()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem();

			this.SetModulesWithValidModule();

			this.moduleDataValidator.Setup(foo => foo.ValidatData(It.IsAny<IModuleBase>())).Returns(false);

			var xDoc = new XDocument();
			var xModules = new XElement(LoadModuleSetting.Tag_Modules);
			xModules.Add(this.SetValidModule());
			xDoc.Add(xModules);

			this.fileSystem.Setup(foo => foo.LoadXml(this.path_File)).Returns(xDoc);

			var restult = this.testee.LoadConfig(this.fackInitializeModules.InitializeModule);

			this.CheckError(ErrorIdList.ConfigModuleIO_ModuleDataNotValid, ErrorGravity.FatalApplication);
			Assert.Null(restult);
		}

		[Fact]
		public void LoadConfigWhenChanelDirectionIsWrongThenModulesError_ReadChanel()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem();
			this.moduleDataValidator.Setup(foo => foo.ValidatData(It.IsAny<IModuleBase>())).Returns(true);

			var xDoc = new XDocument();
			var xModules = new XElement(LoadModuleSetting.Tag_Modules);
			xModules.Add(this.SetValidModule());
			var modu = xModules.Element(LoadModuleSetting.Tag_Module);
			if (modu != null)
			{
				var chanel = modu.Element(LoadModuleSetting.Tag_Chanel);
				if (chanel != null)
				{
					chanel.SetAttributeValue(LoadModuleSetting.Chanel_Direction, "DummyValue");
				}
			}
			xDoc.Add(xModules);

			this.fileSystem.Setup(foo => foo.LoadXml(this.path_File)).Returns(xDoc);

			var result = this.testee.LoadConfig(this.fackInitializeModules.InitializeModule);

			this.CheckError(ErrorIdList.ConfigModuleIO_ReadChanel, ErrorGravity.FatalApplication);
			Assert.Null(result);
		}

		[Fact]
		public void LoadConfigWhenChanelTypeOfValueIsWrongThenModulesError_ReadChanel()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem();
			this.moduleDataValidator.Setup(foo => foo.ValidatData(null)).Returns(true);

			var xDoc = new XDocument();
			var xModules = new XElement(LoadModuleSetting.Tag_Modules);
			xModules.Add(this.SetValidModule());
			var modu = xModules.Element(LoadModuleSetting.Tag_Module);
			if (modu != null)
			{
				var chanel = modu.Element(LoadModuleSetting.Tag_Chanel);
				if (chanel != null)
				{
					chanel.SetAttributeValue(LoadModuleSetting.Chanel_TypeOfValue, "DummyValue");
				}
			}

			xDoc.Add(xModules);

			this.fileSystem.Setup(foo => foo.LoadXml(this.path_File)).Returns(xDoc);

			var result = this.testee.LoadConfig(this.fackInitializeModules.InitializeModule);

			this.CheckError(ErrorIdList.ConfigModuleIO_ReadChanel, ErrorGravity.FatalApplication);
			Assert.Null(result);
		}

		#region Help methode
		private void SetupVariable()
		{
			this.path_File = string.Format("{0}\\{1}", PATH, FILE);
			this.pathDll_File = string.Format("{0}{1}", PATH_DLL, FILE);
			this.fackInitializeModules = new FackInitializeModules();
			this.modules.Setup(foo => foo.ModuleList).Returns(new List<IModuleBase>());
		}

		private void SetupFileSystem(bool pathFileResponse = true, bool pathDllFileResponse = true)
		{
			if (pathFileResponse)
			{
				this.fileSystem.Setup(foo => foo.FileExists(this.path_File)).Returns(true);
			}
			if (pathDllFileResponse)
			{
				this.fileSystem.Setup(foo => foo.FileExists(this.pathDll_File)).Returns(true);
			}
			this.fileSystem.Setup(foo => foo.LoadXml(this.path_File)).Returns(new XDocument());
			this.fileSystem.Setup(foo => foo.GetDLLPathForThisClass<LoadModuleSetting>()).Returns(PATH_DLL);
		}

		private void SetupGlobalVariables(string configFilePath, string moduleConfigName)
		{
			this.globalVariables.Setup(foo => foo.ConfigFilePath).Returns(configFilePath);
			this.globalVariables.Setup(foo => foo.ModuleConfigName).Returns(moduleConfigName);
		}

		private XElement SetValidModule(bool addchanel = true)
		{
			var module = new XElement(LoadModuleSetting.Tag_Module);
			module.SetAttributeValue(LoadModuleSetting.Module_Name, MODULE_NAME);
			module.SetAttributeValue(LoadModuleSetting.Module_ModuleSerie, MODULE_SERIE);
			module.SetAttributeValue(LoadModuleSetting.Module_ModuleType, MODULE_TYPE);
			module.SetAttributeValue(LoadModuleSetting.Module_IP, MODULE_IP);
			module.SetAttributeValue(LoadModuleSetting.Module_Port, MODULE_PORT);
			if (addchanel)
			{
				module.Add(this.SetValidChanel());
			}
			return module;
		}

		private XElement SetValidChanel()
		{
			var chanel = new XElement(LoadModuleSetting.Tag_Chanel);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Id, CHANEL_ID);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Key, CHANEL_KEY);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Direction, CHANEL_DIRECTION);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_TypeOfValue, CHANEL_TYPE_OF_VALUE);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Gain, CHANEL_GAIN);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Offset, CHANEL_OFFSET);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Description, CHANEL_DESCRIPTION);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Comment, CHANEL_COMMENT);
			return chanel;
		}

		private void CheckError(ErrorIdList error, ErrorGravity errorGravity = ErrorGravity.Error, int errorPosition = 0)
		{
			Assert.Equal(error, this.errorTracking.Object.Errors[errorPosition].Id);
			Assert.Equal(errorGravity, this.errorTracking.Object.Errors[errorPosition].Gravity);
		}

		private void CheckValidModule(IModules resultModules, int positionModule = 0)
		{
			var module = resultModules.ModuleList[positionModule];
			Assert.NotNull(module);
			Assert.Equal(MODULE_NAME, module.Name);
			Assert.Equal(MODULE_SERIE, module.ModuleSerie);
			Assert.Equal(MODULE_TYPE, module.ModuleType);
			Assert.Equal(MODULE_IP, module.IpAddress);
			Assert.Equal(MODULE_PORT, module.Port);
		}

		private void CheckValidChanel(IModules resultModules, int positionModule = 0, int positionChanel = 0)
		{
			var module = resultModules.ModuleList[positionModule];
			Assert.NotNull(module);
			var chanel = module.Chanels[positionChanel];
			Assert.NotNull(chanel);
			Assert.Equal(CHANEL_ID, chanel.Id);
			Assert.Equal(CHANEL_KEY, chanel.Key);
			Assert.Equal(CHANEL_DIRECTION, chanel.Direction);
			Assert.Equal(CHANEL_TYPE_OF_VALUE, chanel.TypeOfValue);
			Assert.Equal(CHANEL_GAIN, chanel.Gain);
			Assert.Equal(CHANEL_OFFSET, chanel.Offset);
			Assert.Equal(CHANEL_DESCRIPTION, chanel.Description);
			Assert.Equal(CHANEL_COMMENT, chanel.Comment);
		}

		private void SetModulesWithValidModule()
		{
			this.moduleBase = this.fackInitializeModules.ModuleBase.Initialize(MODULE_SERIE, MODULE_TYPE, this.fackInitializeModules.Modules);
			this.moduleBase.IpAddress = MODULE_IP;
			this.moduleBase.Name = MODULE_NAME;
			this.moduleBase.Port = MODULE_PORT;
			this.moduleBase.Chanels = new List<IChanel>
									  {
										  new Chanel(CHANEL_ID, CHANEL_KEY, CHANEL_DIRECTION, CHANEL_TYPE_OF_VALUE) {Gain = CHANEL_GAIN, Offset = CHANEL_OFFSET}
									  };
			this.modules.Setup(foo => foo.ModuleList).Returns(new List<IModuleBase> {this.moduleBase});
		}
		#endregion
	}

	public class FackInitializeModules : InitializeModules
	{
		public FackInitializeModules()
		{
			this.ModuleBase = new ModuleTest();
		}

		public IModuleBase ModuleBase { get; private set; }

		public IModuleBase InitializeModule(string moduleSerie, string moduleType)
		{
			var modules = new Modules();
			this.ModuleBase = new ModuleTest().Initialize(moduleSerie, moduleType, modules);
			return this.ModuleBase;
		}
	}

	public class ModuleTest : ModuleBase
	{
		/// <summary>Initialize the module.</summary>
		public override IModuleBase Initialize(string moduleSerie, string moduleType, IModules modules)
		{
			this.ModuleSerie = moduleSerie;
			this.ModuleType = moduleType;
			return this;
		}

		/// <summary>Connect the module.</summary>
		public override void Start() {}

		/// <summary>Closings module connection.</summary>
		public override void Closing() {}

		/// <summary>Reads the data from the module IO. Place the response value to the Channels list.</summary>
		public override void ReadData() {}

		/// <summary>Write new info to the chanel list and change the value to the ADAM module</summary>
		/// <param name="chanelId" >Chanel Id.</param>
		/// <param name="digitalValue" >Digital value (true/false).</param>
		/// <param name="anamlogValue" >Analog value.</param>
		public override void WriteData(int chanelId, bool? digitalValue, float? anamlogValue = null) {}
	}
}