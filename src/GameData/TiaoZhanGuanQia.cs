using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TiaoZhanGuanQia")]
	[Serializable]
	public class TiaoZhanGuanQia : IExtensible
	{
		private int _id;

		private int _stage;

		private int _lv;

		private int _difficulty;

		private int _noJump;

		private int _rewardId;

		private readonly List<int> _currencyType = new List<int>();

		private readonly List<int> _currencyNum = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "stage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int stage
		{
			get
			{
				return this._stage;
			}
			set
			{
				this._stage = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "difficulty", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int difficulty
		{
			get
			{
				return this._difficulty;
			}
			set
			{
				this._difficulty = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "noJump", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int noJump
		{
			get
			{
				return this._noJump;
			}
			set
			{
				this._noJump = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "rewardId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId
		{
			get
			{
				return this._rewardId;
			}
			set
			{
				this._rewardId = value;
			}
		}

		[ProtoMember(8, Name = "currencyType", DataFormat = DataFormat.TwosComplement)]
		public List<int> currencyType
		{
			get
			{
				return this._currencyType;
			}
		}

		[ProtoMember(9, Name = "currencyNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> currencyNum
		{
			get
			{
				return this._currencyNum;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
