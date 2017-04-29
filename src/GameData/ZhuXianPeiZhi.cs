using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhuXianPeiZhi")]
	[Serializable]
	public class ZhuXianPeiZhi : IExtensible
	{
		[ProtoContract(Name = "RewardPair")]
		[Serializable]
		public class RewardPair : IExtensible
		{
			private int _key;

			private string _value = string.Empty;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.Default), DefaultValue("")]
			public string value
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

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _id;

		private int _chapterId;

		private int _name;

		private int _icon;

		private int _backgroundPic;

		private int _bossInstanceBoss;

		private int _num;

		private int _minLv;

		private int _expendVit;

		private int _suitCapabilitie;

		private readonly List<int> _frontInstance = new List<int>();

		private readonly List<int> _nextInstance = new List<int>();

		private int _linkTask;

		private int _ruleId;

		private readonly List<ZhuXianPeiZhi.RewardPair> _reward = new List<ZhuXianPeiZhi.RewardPair>();

		private readonly List<int> _star = new List<int>();

		private int _instance;

		private readonly List<int> _endTrigger = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = false, Name = "chapterId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterId
		{
			get
			{
				return this._chapterId;
			}
			set
			{
				this._chapterId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "backgroundPic", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int backgroundPic
		{
			get
			{
				return this._backgroundPic;
			}
			set
			{
				this._backgroundPic = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "bossInstanceBoss", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossInstanceBoss
		{
			get
			{
				return this._bossInstanceBoss;
			}
			set
			{
				this._bossInstanceBoss = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "expendVit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int expendVit
		{
			get
			{
				return this._expendVit;
			}
			set
			{
				this._expendVit = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "suitCapabilitie", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suitCapabilitie
		{
			get
			{
				return this._suitCapabilitie;
			}
			set
			{
				this._suitCapabilitie = value;
			}
		}

		[ProtoMember(13, Name = "frontInstance", DataFormat = DataFormat.TwosComplement)]
		public List<int> frontInstance
		{
			get
			{
				return this._frontInstance;
			}
		}

		[ProtoMember(14, Name = "nextInstance", DataFormat = DataFormat.TwosComplement)]
		public List<int> nextInstance
		{
			get
			{
				return this._nextInstance;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "linkTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkTask
		{
			get
			{
				return this._linkTask;
			}
			set
			{
				this._linkTask = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "ruleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ruleId
		{
			get
			{
				return this._ruleId;
			}
			set
			{
				this._ruleId = value;
			}
		}

		[ProtoMember(17, Name = "reward", DataFormat = DataFormat.Default)]
		public List<ZhuXianPeiZhi.RewardPair> reward
		{
			get
			{
				return this._reward;
			}
		}

		[ProtoMember(18, Name = "star", DataFormat = DataFormat.TwosComplement)]
		public List<int> star
		{
			get
			{
				return this._star;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "instance", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int instance
		{
			get
			{
				return this._instance;
			}
			set
			{
				this._instance = value;
			}
		}

		[ProtoMember(20, Name = "endTrigger", DataFormat = DataFormat.TwosComplement)]
		public List<int> endTrigger
		{
			get
			{
				return this._endTrigger;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
