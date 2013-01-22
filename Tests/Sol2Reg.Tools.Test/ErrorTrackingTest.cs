// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools.Test\ErrorTrackingTest.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools.Test\ErrorTrackingTest.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools.Test\ErrorTrackingTest.cs
//     Created            : 21.01.2013 19:47
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools.Test
{
	using System;
	using Sol2Reg.Tools.Error;
	using Xunit;

	public class ErrorTrackingTest
	{
		// Normal test after this point
		private const ErrorIdList ID = ErrorIdList.ConfigModuleIO_NoFile;
		private const ErrorGravity GRAVITY = ErrorGravity.FatalApplication;
		private const string MESSAGE = "Message";

		private readonly ErrorTracking testee;

		/// <summary>
		///     Initializes a new instance of the <see cref="ErrorTrackingTest" /> class.
		/// </summary>
		public ErrorTrackingTest()
		{
			this.testee = new ErrorTracking();
		}

		[Fact] // This test must stay on this place. If the call line (var rep = new ErrorTrackingSimulation().GetCurrentStackTrace()) change, please chane on the answer comp.
		public void GetCurrentStackTrace()
		{
			var rep = new ErrorTrackingSimulation().GetCurrentStackTrace();

			var comp = "Assembly [Sol2Reg.Tools.Test.dll], NameSpace [Sol2Reg.Tools.Test] class [ErrorTrackingSimulation], Method [GetCurrentStackTrace]\n\tFile \"c:\\Users\\Michel\\Projects\\Sol2Reg\\Main\\Source\\Sol2Reg.ShortService\\Tests\\Sol2Reg.Tools.Test\\ErrorTrackingSimulation.cs\" Line/Col (22/4)\nAssembly [Sol2Reg.Tools.Test.dll], NameSpace [Sol2Reg.Tools.Test] class [ErrorTrackingTest], Method [GetCurrentStackTrace]\n\tFile \"c:\\Users\\Michel\\Projects\\Sol2Reg\\Main\\Source\\Sol2Reg.ShortService\\Tests\\Sol2Reg.Tools.Test\\ErrorTrackingTest.cs\" Line/Col (40/4)\n";
			Assert.Equal(comp, rep);
		}

		/// <summary>Add_s the expection when ok then add to the list.</summary>
		public void Add_ExpectionWhenOkThenAddToTheList()
		{
			this.testee.Add(ErrorIdList.ConfigModuleIO_NoFile, ErrorGravity.FatalApplication, new Exception(MESSAGE));

			var result = this.testee.Errors[0];

			CheckErrorDescription(result);
		}

		/// <summary>Add_s the expection when ok then add to the list.</summary>
		public void AddWhenOkThenAddToTheList()
		{
			this.testee.Add(ErrorIdList.ConfigModuleIO_NoFile, ErrorGravity.FatalApplication, new[] {MESSAGE});

			var result = this.testee.Errors[0];

			CheckErrorDescription(result);
		}

		private static void CheckErrorDescription(ErrorDescription result)
		{
			Assert.Equal(ID, result.Id);
			Assert.Equal(GRAVITY, result.Gravity);
			Assert.Equal(1, result.Args.Length);
			Assert.Equal(MESSAGE, result.Args[0]);
		}
	}
}