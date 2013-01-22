// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\ErrorDescription.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\ErrorDescription.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\ErrorDescription.cs
//     Created            : 21.01.2013 19:29
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools.Error
{
	/// <summary>Error description</summary>
	public class ErrorDescription
	{
		/// <summary>Gets or sets the id.</summary>
		/// <value>The id.</value>
		public ErrorIdList Id { get; set; }

		/// <summary>Gets or sets the gravity.</summary>
		/// <value>The gravity.</value>
		public ErrorGravity Gravity { get; set; }

		/// <summary>Gets or sets the args.</summary>
		/// <value>The args.</value>
		public string[] Args { get; set; }

		/// <summary>Gets or sets the stack trace.</summary>
		/// <value>The stack trace.</value>
		public string StackTrace { get; set; }

		/// <summary>Gets or sets the exception message.</summary>
		/// <value>The exception message.</value>
		public string ExceptionMessage { get; set; }
	}
}