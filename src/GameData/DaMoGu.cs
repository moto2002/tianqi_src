using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DaMoGu")]
	[Serializable]
	public class DaMoGu : IExtensible
	{
		private int _gameTimes;

		private int _comboMiss;

		private int _comboTimes;

		private int _comboNum;

		private int _comboMushroom;

		private int _mushroomAddTime;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "gameTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gameTimes
		{
			get
			{
				return this._gameTimes;
			}
			set
			{
				this._gameTimes = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "comboMiss", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int comboMiss
		{
			get
			{
				return this._comboMiss;
			}
			set
			{
				this._comboMiss = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "comboTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int comboTimes
		{
			get
			{
				return this._comboTimes;
			}
			set
			{
				this._comboTimes = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "comboNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int comboNum
		{
			get
			{
				return this._comboNum;
			}
			set
			{
				this._comboNum = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "comboMushroom", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int comboMushroom
		{
			get
			{
				return this._comboMushroom;
			}
			set
			{
				this._comboMushroom = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "mushroomAddTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mushroomAddTime
		{
			get
			{
				return this._mushroomAddTime;
			}
			set
			{
				this._mushroomAddTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
