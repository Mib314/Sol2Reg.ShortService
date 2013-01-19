// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\FileSystem.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\FileSystem.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\FileSystem.cs
//     Created            : 19.01.2013 01:56
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools
{
	using System.ComponentModel.Composition;
	using System.IO;
	using System.Reflection;

	/// <summary>Tools for FileSystem</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class FileSystem
	{
		/// <summary>Gets the DLL path for this class.</summary>
		/// <typeparam name="TClass" >The type of the class.</typeparam>
		/// <returns>Path of the class.</returns>
		public string GetDLLPathForThisClass<TClass>()
		{
			return Path.GetDirectoryName(Assembly.GetAssembly(typeof (TClass)).CodeBase);
		}
	}
}