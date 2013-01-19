// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\ModuleIO.Interface\IChanel.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     ModuleIO.Interface\IChanel.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : ModuleIO.Interface\IChanel.cs
//     Created            : 28.12.2012 04:22
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO.Interface
{
	using System.ComponentModel.Composition;
	using Sol2Reg.ServiceData.Enumerations;

	[InheritedExport]
	public interface IChanel
	{
		/// <summary>Gets the id.</summary>
		/// <value>The id.</value>
		int Id { get; }

		/// <summary>Gets the key.</summary>
		/// <value>The key.</value>
		string Key { get; }

		/// <summary>Gets the direction.</summary>
		/// <value>The direction.</value>
		Direction Direction { get; }

		/// <summary>Gets the type of the value.</summary>
		/// <value>The type of the value.</value>
		TypeOfValue TypeOfValue { get; }

		/// <summary>Gets or sets the description.</summary>
		/// <value>The description.</value>
		string Description { get; set; }

		/// <summary>Gets or sets the value analog. Apply : (ValueAnalogBrut * Gain) + Offset</summary>
		/// <value>The value analog.</value>
		float? ValueAnalog { get; set; }

		/// <summary>Gets or sets the value analog brut.</summary>
		/// <value>The value analog brut.</value>
		float? ValueAnalogBrut { get; set; }

		/// <summary>Gets or sets the value digital.</summary>
		/// <value>The value digital.</value>
		bool? ValueDigital { get; set; }

		/// <summary>Gets or sets the comment.</summary>
		/// <value>The comment.</value>
		string Comment { get; set; }

		/// <summary>Gets or sets the gain (only for analog value).</summary>
		/// <value>The gain (default value = 1).</value>
		float Gain { get; set; }

		/// <summary>Gets or sets the offset (only for analog value).</summary>
		/// <value>The offset (default value = 0).</value>
		float Offset { get; set; }

		/// <summary>
		/// Changes the value.
		/// </summary>
		/// <param name="analogValue">The analog value.</param>
		/// <param name="digitalValue">The digital value.</param>
		void ChangeValue(float? analogValue, bool? digitalValue);

		string ToString();
	}
}