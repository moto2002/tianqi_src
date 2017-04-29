using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShuaXinPinLv")]
	[Serializable]
	public class ShuaXinPinLv : IExtensible
	{
		private int _level;

		private int _time;

		private int _speed;

		private int _frequency;

		private int _bombProbability;

		private int _kingProbability;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "time", DataFormat = DataFormat.TwosComplement)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "speed", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int speed
		{
			get
			{
				return this._speed;
			}
			set
			{
				this._speed = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "frequency", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frequency
		{
			get
			{
				return this._frequency;
			}
			set
			{
				this._frequency = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "bombProbability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bombProbability
		{
			get
			{
				return this._bombProbability;
			}
			set
			{
				this._bombProbability = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "kingProbability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int kingProbability
		{
			get
			{
				return this._kingProbability;
			}
			set
			{
				this._kingProbability = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
