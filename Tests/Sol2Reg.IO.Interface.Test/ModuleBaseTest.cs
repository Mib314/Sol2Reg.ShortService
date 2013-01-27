// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Interface.Test\ModuleBaseTest.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Interface.Test\ModuleBaseTest.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Interface.Test\ModuleBaseTest.cs
//     Created            : 20.01.2013 21:36
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Interface.Test
{
	using Xunit;

	public class ModuleBaseTest
	{
		private readonly string moduleSerie;
		private readonly string moduleType;
		private readonly IModules modules;
		private readonly Implement_ModuleBase testee;


		public ModuleBaseTest()
		{
			this.moduleSerie = "moduleSerie";
			this.moduleType = "moduleType";
			this.modules = new Modules();

			this.testee = new Implement_ModuleBase();
		}

		[Fact]
		public void InitializeWhenSetCorrectDataThenInitializeOk()
		{
			this.testee.Initialize(this.moduleSerie, this.moduleType, this.modules);

			Assert.Equal(this.testee.ModuleSerie, this.moduleSerie);
			Assert.Equal(this.testee.ModuleModel, this.moduleType);
			Assert.Equal(this.testee.Modules.ModuleList.Count, this.modules.ModuleList.Count);
		}
	}
}