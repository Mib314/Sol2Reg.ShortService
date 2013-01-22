// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.IO\Chanel.cs" company="iLog">
//     Copyright © iLog, 2012 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.IO\Chanel.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.IO\Chanel.cs
//     Created            : 27.12.2012 20:05
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.IO
{
	using Sol2Reg.IO.Interface;
	using Sol2Reg.ServiceData.Enumerations;

	/// <summary>Define and store information about a chanel.</summary>
	public class Chanel : IChanel
	{
		private const float EPSILON_VALUE = (float) 0.00001;

		/// <summary>
		///     Initializes a new instance of the <see cref="Chanel" /> class.
		/// </summary>
		/// <param name="id" >The id.</param>
		/// <param name="direction" >The direction.</param>
		/// <param name="typeOfValue" >Type of the value.</param>
		public Chanel(int id, Direction direction, TypeOfValue typeOfValue)
			: this(id, "Chanel [0]" + id, direction, typeOfValue) {}

		/// <summary>
		///     Initializes a new instance of the <see cref="Chanel" /> class.
		/// </summary>
		/// <param name="id" >The id.</param>
		/// <param name="key" >The key.</param>
		/// <param name="direction" >The direction.</param>
		/// <param name="typeOfValue" >Type of the value.</param>
		public Chanel(int id, string key, Direction direction, TypeOfValue typeOfValue)
		{
			this.Id = id;
			this.Key = key;
			this.Direction = direction;
			this.TypeOfValue = typeOfValue;
			if (this.TypeOfValue == TypeOfValue.Digital)
			{
				this.ValueAnalog = 0;
				this.ValueDigital = null;
			}
			else
			{
				this.ValueAnalog = null;
				this.ValueDigital = false;
			}

			this.Gain = 1;
			this.Offset = 0;
		}

		#region IChanel Members
		/// <summary>Gets the id.</summary>
		/// <value>The id.</value>
		public int Id { get; private set; }

		/// <summary>Gets the key.</summary>
		/// <value>The key.</value>
		public string Key { get; private set; }

		/// <summary>Gets the direction.</summary>
		/// <value>The direction.</value>
		public Direction Direction { get; private set; }

		/// <summary>Gets the type of the value.</summary>
		/// <value>The type of the value.</value>
		public TypeOfValue TypeOfValue { get; private set; }

		/// <summary>Gets or sets the description.</summary>
		/// <value>The description.</value>
		public string Description { get; set; }

		/// <summary>Gets or sets the value analog. Apply : (ValueAnalogBrut * Gain) + Offset</summary>
		/// <value>The value analog.</value>
		public float? ValueAnalog
		{
			get { return this.ValueAnalogBrut * this.Gain + this.Offset; }
			set { this.ValueAnalogBrut = (value - this.Offset) / this.Gain; }
		}

		/// <summary>Gets or sets the value analog brut.</summary>
		/// <value>The value analog brut.</value>
		public float? ValueAnalogBrut { get; set; }

		/// <summary>Gets or sets the value digital.</summary>
		/// <value>The value digital.</value>
		public bool? ValueDigital { get; set; }

		/// <summary>Gets or sets the comment.</summary>
		/// <value>The comment.</value>
		public string Comment { get; set; }

		/// <summary>Gets or sets the gain (only for analog value).</summary>
		/// <value>The gain (default value = 1).</value>
		public float Gain { get; set; }

		/// <summary>Gets or sets the offset (only for analog value).</summary>
		/// <value>The offset (default value = 0).</value>
		public float Offset { get; set; }


		/// <summary>Changes the value.</summary>
		/// <param name="analogValue" >The analog value.</param>
		/// <param name="digitalValue" >The digital value.</param>
		public void ChangeValue(float? analogValue, bool? digitalValue)
		{
			if (this.Gain < EPSILON_VALUE)
			{
				this.ValueAnalogBrut = 0;
			}

			this.ValueAnalogBrut = (analogValue - this.Offset) / this.Gain;
		}

		public override string ToString()
		{
			return string.Format("{0} - {1} : dir : {2} - Value {3}", this.Id, this.Key, this.Direction, this.TypeOfValue == TypeOfValue.Analog ? this.ValueAnalog.ToString() : this.ValueDigital.ToString());
		}
		#endregion
	}
}