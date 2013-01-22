// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO.ADAM6000Com\IAdamModule.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO.ADAM6000Com\IAdamModule.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO.ADAM6000Com\IAdamModule.cs
//     Created            : 28.12.2012 22:09
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.ADAM6000Com
{
	using System.ComponentModel.Composition;

	[InheritedExport]
	public interface IAdamModule
	{
		/// <summary>Gets the total chanel analaog in.</summary>
		/// <value>The total chanel analaog in.</value>
		int TotalChanelAnalaogIn { get; }

		/// <summary>Gets the total chanel analaog out.</summary>
		/// <value>The total chanel analaog out.</value>
		int TotalChanelAnalaogOut { get; }

		/// <summary>Gets the total chanel digital out.</summary>
		/// <value>The total chanel digital out.</value>
		int TotalChanelDigitalOut { get; }

		/// <summary>Gets the total chanel digital in.</summary>
		/// <value>The total chanel digital in.</value>
		int TotalChanelDigitalIn { get; }

		/// <summary>Gets the id sart for input chanel.</summary>
		/// <value>The id sart for input chanel.</value>
		int IdSartForInputChanel { get; }

		/// <summary>Gets the id sart for output chanel.</summary>
		/// <value>The id sart for output chanel.</value>
		int IdSartForOutputChanel { get; }
	}
}