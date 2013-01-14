// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.Supervisor\ChanelInfo.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.Supervisor\ChanelInfo.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.Supervisor\ChanelInfo.cs
//     Created            : 07.01.2013 00:15
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.Supervisor.Model
{
	using Sol2Reg.ServiceData.Enumerations;
	using Sol2Reg.Supervisor.Utility;

	/// <summary>
	/// Chanel model with configuration information
	/// </summary>
	public class ChanelInfoModel : S2RViewModelBase<ChanelInfoModel>
	{
		private float offset;
		private int id;
		private string key;
		private Direction direction;
		private TypeOfValue typeOfValue;
		private float gain;

		public int Id
		{
			get { return this.id; }
			set
			{
				if (value == this.id) return;
				this.id = value;
				this.RaisePropertyChanged(foo => foo.Id);
			}
		}

		public string Key
		{
			get { return this.key; }
			set
			{
				if (value == this.key) return;
				this.key = value;
				this.RaisePropertyChanged(foo => foo.Key);
			}
		}

		public Direction Direction
		{
			get { return this.direction; }
			set
			{
				if (value == this.direction) return;
				this.direction = value;
				this.RaisePropertyChanged(foo => foo.Direction);
			}
		}

		public TypeOfValue TypeOfValue
		{
			get { return this.typeOfValue; }
			set
			{
				if (value == this.typeOfValue) return;
				this.typeOfValue = value;
				this.RaisePropertyChanged(foo => foo.TypeOfValue);
			}
		}

		public float Gain
		{
			get { return this.gain; }
			set
			{
				if (value.Equals(this.gain)) return;
				this.gain = value;
				this.RaisePropertyChanged(foo => foo.Gain);
			}
		}

		public float Offset
		{
			get { return this.offset; }
			set
			{
				if (value.Equals(this.offset)) return;
				this.offset = value;
				this.RaisePropertyChanged(foo => foo.Gain);
			}
		}
	}
}