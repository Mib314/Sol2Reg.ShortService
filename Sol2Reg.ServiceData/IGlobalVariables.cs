// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ServiceData\IGlobalVariables.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ServiceData\IGlobalVariables.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ServiceData\IGlobalVariables.cs
//     Created            : 21.01.2013 10:33
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData
{
	public interface IGlobalVariables
	{
		/// <summary>Gets the config file path.</summary>
		/// <value>The config file path.</value>
		string ConfigFilePath { get; }

		/// <summary>Gets the name of the module config.</summary>
		/// <value>The name of the module config.</value>
		string ModuleConfigName { get; }

		/// <summary>Initializes this instance.</summary>
		void Initialize();
	}
}