// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.Interface\InitializerBase.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.Interface\InitializerBase.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.Interface\InitializerBase.cs
//     Created            : 20.01.2013 18:17
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Interface
{
	public abstract class InitializerBase : IInitializer
	{
		#region IInitializer Members
		/// <summary>Gets the module serie key.</summary>
		/// <value>The module serie key.</value>
		public string ModuleSerie_Key
		{
			get { return this.GetType().ToString(); }
		}

		/// <summary>Initialyses the module.</summary>
		public abstract IModuleBase InitializeModule(string moduleSerie, string moduleType, IModules modules);
		#endregion

		public static string GetKey<TInitializer>()
		{
			return typeof (TInitializer).ToString();
		}
	}
}