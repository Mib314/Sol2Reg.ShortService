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
	using System.Xml.Linq;

	/// <summary>Tools for FileSystem</summary>
	[PartCreationPolicy(CreationPolicy.Shared)]
	[Export]
	public class FileSystem : IFileSystem
	{
		#region IFileSystem Members
		/// <summary>Gets the DLL path for this class.</summary>
		/// <typeparam name="TClass" >The type of the class.</typeparam>
		/// <returns>Path of the class.</returns>
		public string GetDLLPathForThisClass<TClass>()
		{
			return Path.GetDirectoryName(Assembly.GetAssembly(typeof (TClass)).CodeBase);
		}

		/// <summary>Files the exists.</summary>
		/// <param name="path" >The path.</param>
		/// <returns>File existe => true othervise false.</returns>
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		/// <summary>Loads the XML.</summary>
		/// <param name="path" >The path.</param>
		/// <returns>XDocument from the file</returns>
		public XDocument LoadXml(string path)
		{
			if (File.Exists(path))
			{
				return XDocument.Load(path);
			}
			return null;
		}
		#endregion
	}
}