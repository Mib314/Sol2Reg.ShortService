// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Test\InitializeModulesTest.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Test\InitializeModulesTest.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Test\InitializeModulesTest.cs
//     Created            : 20.01.2013 14:37
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Test
{
	using System.Collections.Generic;
	using System.Linq;
	using Moq;
	using Sol2Reg.IO.ADAM6000Com;
	using Sol2Reg.IO.Interface;
	using Sol2Reg.IO.Simulator;
	using Sol2Reg.Test.Tools;
	using Sol2Reg.Tools.Error;
	using Xunit;

	public class InitializeModulesTest
	{
		private readonly IList<Mock<IInitializer>> initializers;
		private readonly Mock<LoadModuleSetting> loadModuleSetting;
		private Mock<ErrorTracking> errorTracking;
		private Mock<IModules> modules;
		private IList<Mock<IModuleBase>> moduleList;

		//private readonly IModules modules;

		private readonly InitializeModules testee;

		public InitializeModulesTest()
		{
			//this.modules = new ModulesValidForTest().GetModules();
			this.modules = new Mock<IModules>();
			this.moduleList = new List<Mock<IModuleBase>>();
			this.initializers = new List<Mock<IInitializer>>();
			this.loadModuleSetting = new Mock<LoadModuleSetting>();
			this.errorTracking = new Mock<ErrorTracking>();

			this.testee = new InitializeModules {LoadModuleSetting = this.loadModuleSetting.Object, ErrorTracking = this.errorTracking.Object};
		}

		[Fact]
		public void InitializeWhenIsForSimulatorAndNoSimulatorThenError_NoSimulatorInitializer()
		{
			this.SetInitializerForSimulator(false);

			this.testee.Initialize(true);

			ToolsForError.CheckError(this.errorTracking,ErrorIdList.InitilizeModule_NoSimulatorInitializer, ErrorGravity.FatalApplication);
		}

		[Fact]
		public void InitializeWhenIsNotForSimulatorAndSimulatorNotExistThenArgumentNullException()
		{
			this.SetInitializerForSimulator();

			var dd = new ModuleBaseForTest();

			this.modules.Setup(foo => foo.ModuleList).Returns(moduleList);

			this.loadModuleSetting.Object.LoadConfig((x, y) => dd.Initialize(x, y, this.modules.Object));

			this.testee.Initialize(true);

			ToolsForError.CheckNoError(this.errorTracking);
		}

		#region internal function
		private void SetInitializerForSimulator(bool setSimulatorInitializer = true, bool setProductiveInitializer = true)
		{
			if (setSimulatorInitializer)
			{
				var ini1 = new Mock<IInitializer>();
				ini1.Setup(foo => foo.ModuleSerie_Key).Returns(InitializerBase.GetKey<InitializerSimulator>());
				this.initializers.Add(ini1);
			}
			if (setProductiveInitializer)
			{
				var ini1 = new Mock<IInitializer>();
				ini1.Setup(foo => foo.ModuleSerie_Key).Returns(InitializerBase.GetKey<InitializerAdam6000>());
				this.initializers.Add(ini1);
			}
			this.SetInitializerIntoTestee();
		}

		private void SetInitializerIntoTestee()
		{
			var init = this.initializers.Select(initializer => initializer.Object).ToList();
			this.testee.Initializers = init;
		}


		#endregion
	}
}