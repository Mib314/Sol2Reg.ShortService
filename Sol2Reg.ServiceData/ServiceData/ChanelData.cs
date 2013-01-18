// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.ServiceData\ChanelData.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.ServiceData\ChanelData.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.ServiceData\ChanelData.cs
//     Created            : 31.12.2012 16:42
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.ServiceData.ServiceData
{
	using System.Runtime.Serialization;
	using Sol2Reg.ServiceData.Enumerations;

	/// <summary>
	/// Chanel data for WCF service.
	/// </summary>
	[DataContract]
	public class ChanelData
	{
		/// <summary>Gets the id.</summary>
		/// <value>The id.</value>
		[DataMember(Name = "I", IsRequired = true, Order = 0)]
		public int Id { get; set; }

		/// <summary>Gets the key.</summary>
		/// <value>The key.</value>
		[DataMember(Name = "K", IsRequired = false, Order = 1)]
		public string Key { get; set; }

		/// <summary>Gets the direction.</summary>
		/// <value>The direction.</value>
		[DataMember(Name = "DIR", IsRequired = false, Order = 10)]
		public Direction Direction { get; set; }

		/// <summary>Gets the type of the value.</summary>
		/// <value>The type of the value.</value>
		[DataMember(Name = "TOV", IsRequired = false, Order = 11)]
		public TypeOfValue TypeOfValue { get; set; }

		/// <summary>Gets or sets the description.</summary>
		/// <value>The description.</value>
		[DataMember(Name = "DES", IsRequired = false, Order = 2)]
		public string Description { get; set; }

		/// <summary>Gets or sets the value analog. Apply : (ValueAnalogBrut * Gain) + Offset</summary>
		/// <value>The value analog.</value>
		[DataMember(Name = "VA", IsRequired = false, Order = 30)]
		public float? ValueAnalog { get; set; }

		/// <summary>Gets or sets the value analog brut.</summary>
		/// <value>The value analog brut.</value>
		[DataMember(Name = "VAB", IsRequired = false, Order = 31)]
		public float? ValueAnalogBrut { get; set; }

		/// <summary>Gets or sets the value digital.</summary>
		/// <value>The value digital.</value>
		[DataMember(Name = "VD", IsRequired = false, Order = 35)]
		public bool? ValueDigital { get; set; }

		/// <summary>Gets or sets the comment.</summary>
		/// <value>The comment.</value>
		[DataMember(Name = "COM", IsRequired = false, Order = 3)]
		public string Comment { get; set; }

		/// <summary>Gets or sets the gain (only for analog value).</summary>
		/// <value>The gain (default value = 1).</value>
		[DataMember(Name = "G", IsRequired = false, Order = 32)]
		public float Gain { get; set; }

		/// <summary>Gets or sets the offset (only for analog value).</summary>
		/// <value>The offset (default value = 0).</value>
		[DataMember(Name = "O", IsRequired = false, Order = 33)]
		public float Offset { get; set; }


		public override string ToString()
		{
			return string.Format("{0} - {1} : dir : {2} - Value {3}", this.Id, this.Key, this.Direction, this.TypeOfValue == TypeOfValue.Analog ? this.ValueAnalog.ToString() : this.ValueDigital.ToString());
		}
	}
}