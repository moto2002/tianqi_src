using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TaoZhuangDuanZhu")]
	[Serializable]
	public class TaoZhuangDuanZhu : IExtensible
	{
		[ProtoContract(Name = "SuitcostPair")]
		[Serializable]
		public class SuitcostPair : IExtensible
		{
			private int _key;

			private int _value;

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

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _id;

		private int _suitId;

		private int _suitStep;

		private int _suitQuality;

		private readonly List<int> _suitmagaNum = new List<int>();

		private int _suitNeedNum;

		private readonly List<TaoZhuangDuanZhu.SuitcostPair> _suitcost = new List<TaoZhuangDuanZhu.SuitcostPair>();

		private int _effectType;

		private readonly List<int> _skillId = new List<int>();

		private int _suitattr;

		private int _suitName;

		private int _suitMark;

		private string _frame = string.Empty;

		private int _fxId;

		private int _skillDesc;

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

		[ProtoMember(3, IsRequired = false, Name = "suitId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suitId
		{
			get
			{
				return this._suitId;
			}
			set
			{
				this._suitId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "suitStep", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suitStep
		{
			get
			{
				return this._suitStep;
			}
			set
			{
				this._suitStep = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "suitQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suitQuality
		{
			get
			{
				return this._suitQuality;
			}
			set
			{
				this._suitQuality = value;
			}
		}

		[ProtoMember(6, Name = "suitmagaNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> suitmagaNum
		{
			get
			{
				return this._suitmagaNum;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "suitNeedNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suitNeedNum
		{
			get
			{
				return this._suitNeedNum;
			}
			set
			{
				this._suitNeedNum = value;
			}
		}

		[ProtoMember(8, Name = "suitcost", DataFormat = DataFormat.Default)]
		public List<TaoZhuangDuanZhu.SuitcostPair> suitcost
		{
			get
			{
				return this._suitcost;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "effectType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effectType
		{
			get
			{
				return this._effectType;
			}
			set
			{
				this._effectType = value;
			}
		}

		[ProtoMember(10, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public List<int> skillId
		{
			get
			{
				return this._skillId;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "suitattr", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suitattr
		{
			get
			{
				return this._suitattr;
			}
			set
			{
				this._suitattr = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "suitName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suitName
		{
			get
			{
				return this._suitName;
			}
			set
			{
				this._suitName = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "suitMark", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int suitMark
		{
			get
			{
				return this._suitMark;
			}
			set
			{
				this._suitMark = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "frame", DataFormat = DataFormat.Default), DefaultValue("")]
		public string frame
		{
			get
			{
				return this._frame;
			}
			set
			{
				this._frame = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "fxId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fxId
		{
			get
			{
				return this._fxId;
			}
			set
			{
				this._fxId = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "skillDesc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillDesc
		{
			get
			{
				return this._skillDesc;
			}
			set
			{
				this._skillDesc = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
