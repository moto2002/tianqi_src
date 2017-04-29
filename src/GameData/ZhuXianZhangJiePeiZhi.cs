using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhuXianZhangJiePeiZhi")]
	[Serializable]
	public class ZhuXianZhangJiePeiZhi : IExtensible
	{
		[ProtoContract(Name = "RewarditemPair")]
		[Serializable]
		public class RewarditemPair : IExtensible
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

		[ProtoContract(Name = "RewardnumPair")]
		[Serializable]
		public class RewardnumPair : IExtensible
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

		private int _chapterType;

		private int _chapterOrder;

		private int _chapterName;

		private int _chapterLv;

		private readonly List<int> _arrowPosition = new List<int>();

		private int _mainSenceIcon;

		private readonly List<int> _mainSenceIconPoint = new List<int>();

		private readonly List<int> _needStar = new List<int>();

		private readonly List<int> _ruleId = new List<int>();

		private readonly List<ZhuXianZhangJiePeiZhi.RewarditemPair> _rewardItem = new List<ZhuXianZhangJiePeiZhi.RewarditemPair>();

		private readonly List<ZhuXianZhangJiePeiZhi.RewardnumPair> _rewardNum = new List<ZhuXianZhangJiePeiZhi.RewardnumPair>();

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

		[ProtoMember(3, IsRequired = false, Name = "chapterType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterType
		{
			get
			{
				return this._chapterType;
			}
			set
			{
				this._chapterType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "chapterOrder", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterOrder
		{
			get
			{
				return this._chapterOrder;
			}
			set
			{
				this._chapterOrder = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "chapterName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterName
		{
			get
			{
				return this._chapterName;
			}
			set
			{
				this._chapterName = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "chapterLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chapterLv
		{
			get
			{
				return this._chapterLv;
			}
			set
			{
				this._chapterLv = value;
			}
		}

		[ProtoMember(7, Name = "arrowPosition", DataFormat = DataFormat.TwosComplement)]
		public List<int> arrowPosition
		{
			get
			{
				return this._arrowPosition;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "mainSenceIcon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mainSenceIcon
		{
			get
			{
				return this._mainSenceIcon;
			}
			set
			{
				this._mainSenceIcon = value;
			}
		}

		[ProtoMember(9, Name = "mainSenceIconPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> mainSenceIconPoint
		{
			get
			{
				return this._mainSenceIconPoint;
			}
		}

		[ProtoMember(10, Name = "needStar", DataFormat = DataFormat.TwosComplement)]
		public List<int> needStar
		{
			get
			{
				return this._needStar;
			}
		}

		[ProtoMember(11, Name = "ruleId", DataFormat = DataFormat.TwosComplement)]
		public List<int> ruleId
		{
			get
			{
				return this._ruleId;
			}
		}

		[ProtoMember(12, Name = "rewardItem", DataFormat = DataFormat.Default)]
		public List<ZhuXianZhangJiePeiZhi.RewarditemPair> rewardItem
		{
			get
			{
				return this._rewardItem;
			}
		}

		[ProtoMember(13, Name = "rewardNum", DataFormat = DataFormat.Default)]
		public List<ZhuXianZhangJiePeiZhi.RewardnumPair> rewardNum
		{
			get
			{
				return this._rewardNum;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
