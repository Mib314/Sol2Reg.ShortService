﻿
namespace ADAM6000Com
{
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
