// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Interface.Test\InitializerBaseTest.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Interface.Test\InitializerBaseTest.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Interface.Test\InitializerBaseTest.cs
//     Created            : 20.01.2013 18:58
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Interface.Test
{
	using Xunit;

	public class InitializerBaseTest
	{
		private readonly string implementString;
		private readonly InitializerBase testee;

		public InitializerBaseTest()
		{
			this.implementString = new ImplementInitializerBase().ToString();
			this.testee = new ImplementInitializerBase();
		}

		[Fact]
		public void GetKeyWhenStaticTypeOkThenTypeNameOk()
		{
			var key = InitializerBase.GetKey<ImplementInitializerBase>();

			Assert.Equal(key, this.implementString);
		}

		[Fact]
		public void ModuleSerie_KeyWhenDerivedTypeThenNameOk()
		{
			var key = this.testee.ModuleSerie_Key;

			Assert.Equal(key, this.implementString);
		}
	}
}