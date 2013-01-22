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
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using FluentAssertions;
	using Moq;
	using Sol2Reg.IO.ADAM6000Com;
	using Sol2Reg.IO.Interface;
	using Sol2Reg.IO.Simulator;
	using Xunit;

	public class InitializeModulesTest
	{
		private readonly IList<Mock<IInitializer>> initializers;
		private readonly Mock<LoadModuleSetting> loadModuleSetting;
		private readonly InitializeModules testee;

		public InitializeModulesTest()
		{
			this.initializers = new List<Mock<IInitializer>>();
			this.loadModuleSetting = new Mock<LoadModuleSetting>();

			this.testee = new InitializeModules {LoadModuleSetting = this.loadModuleSetting.Object};
		}

		[Fact]
		public void InitializeWhenIsForSimulatorAndSimulatorExistThenNotThrowArgumentNullException()
		{
			this.SetInitializerForSimulator();

			Action action = () => this.testee.Initialize(true);

			action.ShouldNotThrow<ArgumentNullException>();
		}

		[Fact]
		public void InitializeWhenIsNotForSimulatorAndSimulatorExistThenArgumentNullException()
		{
			this.SetInitializerForSimulator(false);

			Action action = () => this.testee.Initialize(true);

			action.ShouldThrow<ArgumentNullException>();
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