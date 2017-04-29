using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "FanPaiPingFenSheZhi")]
	[Serializable]
	public class FanPaiPingFenSheZhi : IExtensible
	{
		private int _time;

		private int _value;

		private int _reward;

		private int _num;

		private string _imgScore = string.Empty;

		private string _imgTreasure = string.Empty;

		private readonly List<int> _rewardItem = new List<int>();

		private readonly List<int> _rewardNum = new List<int>();

		private int _rewardID;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "time", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "reward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reward
		{
			get
			{
				return this._reward;
			}
			set
			{
				this._reward = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "imgScore", DataFormat = DataFormat.Default), DefaultValue("")]
		public string imgScore
		{
			get
			{
				return this._imgScore;
			}
			set
			{
				this._imgScore = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "imgTreasure", DataFormat = DataFormat.Default), DefaultValue("")]
		public string imgTreasure
		{
			get
			{
				return this._imgTreasure;
			}
			set
			{
				this._imgTreasure = value;
			}
		}

		[ProtoMember(8, Name = "rewardItem", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardItem
		{
			get
			{
				return this._rewardItem;
			}
		}

		[ProtoMember(9, Name = "rewardNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardNum
		{
			get
			{
				return this._rewardNum;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "rewardID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardID
		{
			get
			{
				return this._rewardID;
			}
			set
			{
				this._rewardID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
