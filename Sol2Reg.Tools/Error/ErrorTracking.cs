// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\ErrorTracking.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\ErrorTracking.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\ErrorTracking.cs
//     Created            : 21.01.2013 19:26
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools.Error
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Diagnostics;
	using System.Linq;
	using System.Reflection;
	using System.Text;

	/// <summary>Error tracking (singelton)</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class ErrorTracking
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ErrorTracking" /> class.
		/// </summary>
		public ErrorTracking()
		{
			this.Errors = new List<ErrorDescription>();
		}

		/// <summary>Gets or sets the errors.</summary>
		/// <value>The errors.</value>
		public IList<ErrorDescription> Errors { get; set; }

		/// <summary>
		/// Adds the specified error id.
		/// </summary>
		/// <param name="errorId">The error id.</param>
		/// <param name="errorGravity">The error gravity.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="args">The args.</param>
		public void Add(ErrorIdList errorId, ErrorGravity errorGravity, Exception exception, string[] args = null)
		{
			this.Add(new ErrorDescription { Id = errorId, Gravity = errorGravity, ExceptionMessage = exception.Message, StackTrace = exception.StackTrace, Args = args });
		}

		/// <summary>
		/// Adds the specified error id.
		/// </summary>
		/// <param name="errorId">The error id.</param>
		/// <param name="errorGravity">The error gravity.</param>
		/// <param name="args">The args (used with the error id translation).</param>
		public void Add(ErrorIdList errorId, ErrorGravity errorGravity, string[] args = null)
		{
			this.Add(new ErrorDescription { Id = errorId, Gravity = errorGravity, Args = args, StackTrace = GetCurrentStackTrace() });
		}

		/// <summary>Adds the specified error.</summary>
		/// <param name="error" >The error.</param>
		private void Add(ErrorDescription error)
		{
			//Check if this Error with this gravity exist.
			// if (!this.Errors.Any(foo => foo.Id == error.Id && foo.Gravity == error.Gravity)) 
			this.Errors.Add(error);
		}

		/// <summary>Gets the current stack trace.</summary>
		/// <returns>Stack trace.</returns>
		public static string GetCurrentStackTrace(int offsetPorition = 2)
		{
			var stackTrace = new StackTrace(true);
			var frames = stackTrace.GetFrames();
			if (frames == null)
			{
				return string.Empty;
			}

			var myFrames = frames.Where(foo => !string.IsNullOrWhiteSpace(foo.GetFileName())).Select(foo => new Tuple<string, int, int, MethodBase>(foo.GetFileName(), foo.GetFileLineNumber(), foo.GetFileColumnNumber(), foo.GetMethod()));

			if (!myFrames.Any())
			{
				return string.Empty;
			}

			var stackTraceValue = new StringBuilder(200 * myFrames.Count());

			var position = 0;
			foreach (var source in myFrames)
			{
				position++;
				if (position > offsetPorition)
				{
					string method = string.Empty, nameSpace = string.Empty;

					if (source.Item4.DeclaringType != null)
					{
						method = source.Item4.DeclaringType.Name;
						nameSpace = source.Item4.DeclaringType.Namespace;
					}

					stackTraceValue.AppendFormat("Assembly [{0}], NameSpace [{1}] class [{2}], Method [{3}]\n\tFile \"{4}\" Line/Col ({5}/{6})\n", source.Item4.Module, nameSpace, method, source.Item4.Name, source.Item1, source.Item2, source.Item3);
				}
			}
			return stackTraceValue.ToString();
		}
	}
}