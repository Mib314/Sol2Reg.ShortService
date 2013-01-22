// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools.Test\ErrorTrackingSimulation.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools.Test\ErrorTrackingSimulation.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools.Test\ErrorTrackingSimulation.cs
//     Created            : 21.01.2013 22:50
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools.Test
{
	using Sol2Reg.Tools.Error;

	public class ErrorTrackingSimulation
	{
		public string GetCurrentStackTrace()
		{
			return ErrorTracking.GetCurrentStackTrace(1);
		}
	}
}