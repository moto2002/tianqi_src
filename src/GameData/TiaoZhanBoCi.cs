using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TiaoZhanBoCi")]
	[Serializable]
	public class TiaoZhanBoCi : IExtensible
	{
		private int _id;

		private int _refreshId;

		private int _stage;

		private string _stageNum = string.Empty;

		private int _bossId;

		private int _time;

		private int _rewardId;

		private readonly List<int> _currencyType = new List<int>();

		private readonly List<int> _currencyNum = new List<int>();

		private int _rewardId2;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "refreshId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshId
		{
			get
			{
				return this._refreshId;
			}
			set
			{
				this._refreshId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "stage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "stageNum", DataFormat = DataFormat.Default), DefaultValue("")]
		public string stageNum
		{
			get
			{
				return this._stageNum;
			}
			set
			{
				this._stageNum = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "bossId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossId
		{
			get
			{
				return this._bossId;
			}
			set
			{
				this._bossId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "rewardId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, Name = "currencyType", DataFormat = DataFormat.TwosComplement)]
		public List<int> currencyType
		{
			get
			{
				return this._currencyType;
			}
		}

		[ProtoMember(10, Name = "currencyNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> currencyNum
		{
			get
			{
				return this._currencyNum;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "rewardId2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId2
		{
			get
			{
				return this._rewardId2;
			}
			set
			{
				this._rewardId2 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
