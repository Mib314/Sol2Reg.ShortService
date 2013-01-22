// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\ErrorGravity.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\ErrorGravity.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\ErrorGravity.cs
//     Created            : 21.01.2013 19:27
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools.Error
{
	/// <summary>
	/// Error gravity.
	/// </summary>
	public enum ErrorGravity
	{
		/// <summary>
		/// The fatal application (Stop this service).
		/// </summary>
		FatalApplication = 0,
		/// <summary>
		/// The fatal call (the call is out).
		/// </summary>
		FatalCall = 1,
		/// <summary>
		/// The error
		/// </summary>
		Error = 2,
		/// <summary>
		/// The warning
		/// </summary>
		Warning = 3,
		/// <summary>
		/// The info
		/// </summary>
		Info = 4,
		/// <summary>
		/// The debug
		/// </summary>
		Debug = 5
	}
}