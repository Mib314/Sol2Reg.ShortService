namespace ModuleIO
{
	public class ChanelData
	{
		public ChanelData(int id, Direction direction, ValueType valueType)
			: this(id, string.Format("Chanel [0]", id), direction, valueType)
		{ }

		public ChanelData(int id, string key, Direction direction, ValueType valueType)
		{
			this.Id = id;
			this.Direction = direction;
			this.ValueType = valueType;
			if (this.ValueType == ValueType.Digital)
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

		public int Id { get; private set; }
		public string Key { get; private set; }
		public Direction Direction { get; private set; }
		public ValueType ValueType { get; private set; }

		public string Description { get; set; }
		public float? ValueAnalog { get; set; }
		public bool? ValueDigital { get; set; }

		public float Gain { get; set; }
		public float Offset { get; set; }

	}
}
