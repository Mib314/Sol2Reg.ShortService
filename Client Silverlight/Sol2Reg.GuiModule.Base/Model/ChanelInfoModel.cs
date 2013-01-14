// ----------------------------------------------------------------------------------
// <copyright file="Sol2Reg.ShortService\Sol2Reg.GuiModule.Base\ChanelInfoModel.cs" company="iLog">
//     Copyright © iLog, 2013 . All rights reserved.
// </copyright>
// <summary>
//     Sol2Reg.GuiModule.Base\ChanelInfoModel.cs.
// </summary>
// <FileInfo>
//     Project \ FileName : Sol2Reg.GuiModule.Base\ChanelInfoModel.cs
//     Created            : 12.01.2013 16:38
// </FileInfo>
//  ----------------------------------------------------------------------------------

namespace Sol2Reg.GuiModule.Base.Model
{
	using Sol2Reg.ServiceData.Enumerations;
	using Sol2Reg.Supervisor.Utility;

	/// <summary>Chanel model with configuration information</summary>
	public class ChanelInfoModel : S2RViewModelBase<ChanelInfoModel>
	{
		private Direction direction;
		private float gain;
		private int id;
		private string key;
		private float offset;
		private TypeOfValue typeOfValue;

		public int Id
		{
			get { return this.IsInDesignMode ? 0 : this.id; }
			set
			{
				if (value == this.id) return;
				this.id = value;
				this.RaisePropertyChanged(foo => foo.Id);
			}
		}

		public string Key
		{
			get { return this.IsInDesignMode ? "T1" : this.key; }
			set
			{
				if (value == this.key) return;
				this.key = value;
				this.RaisePropertyChanged(foo => foo.Key);
			}
		}

		public Direction Direction
		{
			get { return this.IsInDesignMode ? Direction.Input : this.direction; }
			set
			{
				if (value == this.direction) return;
				this.direction = value;
				this.RaisePropertyChanged(foo => foo.Direction);
			}
		}

		public TypeOfValue TypeOfValue
		{
			get { return this.IsInDesignMode ? TypeOfValue.Analog : this.typeOfValue; }
			set
			{
				if (value == this.typeOfValue) return;
				this.typeOfValue = value;
				this.RaisePropertyChanged(foo => foo.TypeOfValue);
			}
		}

		public float Gain
		{
			get { return this.IsInDesignMode ? 1 : this.gain; }
			set
			{
				if (value.Equals(this.gain)) return;
				this.gain = value;
				this.RaisePropertyChanged(foo => foo.Gain);
			}
		}

		public float Offset
		{
			get { return this.IsInDesignMode ? 0 : this.offset; }
			set
			{
				if (value.Equals(this.offset)) return;
				this.offset = value;
				this.RaisePropertyChanged(foo => foo.Gain);
			}
		}
	}
}