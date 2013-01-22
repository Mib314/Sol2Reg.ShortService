// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Tools\IFileSystem.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Tools\IFileSystem.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Tools\IFileSystem.cs
//     Created            : 21.01.2013 10:32
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Tools
{
	using System.Xml.Linq;

	public interface IFileSystem
	{
		/// <summary>Gets the DLL path for this class.</summary>
		/// <typeparam name="TClass" >The type of the class.</typeparam>
		/// <returns>Path of the class.</returns>
		string GetDLLPathForThisClass<TClass>();

		/// <summary>Files the exists.</summary>
		/// <param name="path" >The path.</param>
		/// <returns>File existe => true othervise false.</returns>
		bool FileExists(string path);

		/// <summary>Loads the XML.</summary>
		/// <param name="path" >The path.</param>
		/// <returns>XDocument from the file</returns>
		XDocument LoadXml(string path);
	}
}