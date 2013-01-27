// ----------------------------------------------------------------------------------
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
	using Sol2Reg.Test.Tools;
	using Sol2Reg.Tools;
	using Sol2Reg.Tools.Error;
	using Xunit;

	public class LoadModuleSettingTest
	{
		private const string PATH = "Path";
		private const string FILE = "FILETEST";
		private const string PATH_DLL = "PathDLL\\";

		private readonly Mock<ErrorTracking> errorTracking;


		private readonly Mock<IFileSystem> fileSystem;
		private readonly Mock<IGlobalVariables> globalVariables;
		private readonly Mock<ModuleDataValidator> moduleDataValidator;
		private readonly Mock<IModules> modules;
		private readonly ModulesValidForTest modulesValidForTest;

		private readonly LoadModuleSetting testee;
		private readonly XmlLinq xmlLinq;
		private FackInitializeModules fackInitializeModules;
		private IModuleBase moduleBase;
		private string pathDll_File;
		private string path_File;

		public LoadModuleSettingTest()
		{
			this.modulesValidForTest = new ModulesValidForTest();
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

			ToolsForError.CheckNoError(this.errorTracking);
			Assert.NotNull(result);
			ModulesValidForTest.CheckValidModule(result);
			ModulesValidForTest.CheckValidChanel(result);
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
			ModulesValidForTest.CheckValidModule(result);
			ModulesValidForTest.CheckValidChanel(result);
		}

		[Fact]
		public void LoadConfigWhenFileNotExistThenErrorNoFile()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem(false, false);
			var restult = this.testee.LoadConfig(null);

			ToolsForError.CheckError(this.errorTracking, ErrorIdList.ConfigModuleIO_NoFile, ErrorGravity.FatalApplication);
			Assert.Null(restult);
		}

		[Fact]
		public void LoadConfigWhenFileNotWellForematedThenErrorFileBadFormated()
		{
			this.SetupGlobalVariables(PATH, FILE);
			this.SetupFileSystem();
			this.fileSystem.Setup(foo => foo.LoadXml(this.path_File)).Throws<InvalidDataException>();

			var restult = this.testee.LoadConfig(null);

			ToolsForError.CheckError(this.errorTracking, ErrorIdList.ConfigModuleIO_FileBadFormated, ErrorGravity.FatalApplication);
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

			ToolsForError.CheckError(this.errorTracking, ErrorIdList.ConfigModuleIO_NoTagModules, ErrorGravity.FatalApplication);
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

			ToolsForError.CheckError(this.errorTracking, ErrorIdList.ConfigModuleIO_NoTagModule, ErrorGravity.FatalApplication);
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

			ToolsForError.CheckError(this.errorTracking, ErrorIdList.ConfigModuleIO_ModuleDataNotValid, ErrorGravity.FatalApplication);
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

			ToolsForError.CheckError(this.errorTracking, ErrorIdList.ConfigModuleIO_ReadChanel, ErrorGravity.FatalApplication);
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

			ToolsForError.CheckError(this.errorTracking, ErrorIdList.ConfigModuleIO_ReadChanel, ErrorGravity.FatalApplication);
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
			module.SetAttributeValue(LoadModuleSetting.Module_Name, ModulesValidForTest.Module_Name);
			module.SetAttributeValue(LoadModuleSetting.Module_ModuleSerie, ModulesValidForTest.Module_Serie);
			module.SetAttributeValue(LoadModuleSetting.Module_ModuleType, ModulesValidForTest.Module_Type);
			module.SetAttributeValue(LoadModuleSetting.Module_IP, ModulesValidForTest.Module_IP);
			module.SetAttributeValue(LoadModuleSetting.Module_Port, ModulesValidForTest.Module_Port);
			if (addchanel)
			{
				module.Add(this.SetValidChanel());
			}
			return module;
		}

		private XElement SetValidChanel()
		{
			var chanel = new XElement(LoadModuleSetting.Tag_Chanel);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Id, ModulesValidForTest.Chanel_Id);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Key, ModulesValidForTest.Chanel_Key);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Direction, ModulesValidForTest.Chanel_Direction);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_TypeOfValue, ModulesValidForTest.Chanel_Type_Of_Value);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Gain, ModulesValidForTest.Chanel_Gain);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Offset, ModulesValidForTest.Chanel_Offset);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Description, ModulesValidForTest.Chanel_Description);
			chanel.SetAttributeValue(LoadModuleSetting.Chanel_Comment, ModulesValidForTest.Chanel_Comment);
			return chanel;
		}

		private void SetModulesWithValidModule()
		{
			this.moduleBase = this.fackInitializeModules.ModuleBase.Initialize(ModulesValidForTest.Module_Serie, ModulesValidForTest.Module_Type, this.fackInitializeModules.Modules);

			ModulesValidForTest.GetModulesValid(this.moduleBase);

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
			this.ModuleModel = moduleType;
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