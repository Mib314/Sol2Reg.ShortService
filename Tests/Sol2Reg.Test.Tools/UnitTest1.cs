// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Test.Tools\UnitTest1.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Test.Tools\UnitTest1.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Test.Tools\UnitTest1.cs
//     Created            : 23.01.2013 20:23
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Test.Tools
{
	using System.Linq;
	using Moq;
	using Sol2Reg.Tools.Error;
	using Xunit;

	public static class ToolsForError
	{
		/// <summary>Checks the error.</summary>
		/// <param name="errorTracking" >The error tracking.</param>
		/// <param name="error" >The error.</param>
		/// <param name="errorGravity" >The error gravity.</param>
		/// <param name="errorPosition" >The error position.</param>
		public static void CheckError(Mock<ErrorTracking> errorTracking, ErrorIdList error, ErrorGravity errorGravity = ErrorGravity.Error, int errorPosition = 0)
		{
			Assert.Equal(error, errorTracking.Object.Errors[errorPosition].Id);
			Assert.Equal(errorGravity, errorTracking.Object.Errors[errorPosition].Gravity);
		}


		/// <summary>Checks the no error.</summary>
		/// <param name="errorTracking" >The error tracking.</param>
		public static void CheckNoError(Mock<ErrorTracking> errorTracking)
		{
			Assert.False(errorTracking.Object.Errors.Any());
		}
	}
}