using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "zZhuangBeiPeiZhiBiao")]
	[Serializable]
	public class zZhuangBeiPeiZhiBiao : IExtensible
	{
		[ProtoContract(Name = "SmeltdropPair")]
		[Serializable]
		public class SmeltdropPair : IExtensible
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

		private string _name = string.Empty;

		private int _quality;

		private int _position;

		private int _occupation;

		private int _level;

		private int _step;

		private int _icon;

		private int _model;

		private int _attrBaseValue;

		private int _attrGrowValue;

		private int _firstGroupId;

		private int _frontID;

		private int _offerEvoExp;

		private int _describe;

		private int _attrNum;

		private int _attrLibrary;

		private string _materialIdAndNum = string.Empty;

		private int _enchantNum;

		private int _starNum;

		private readonly List<int> _boostStarAttr = new List<int>();

		private int _resolveDrop;

		private readonly List<zZhuangBeiPeiZhiBiao.SmeltdropPair> _smeltDrop = new List<zZhuangBeiPeiZhiBiao.SmeltdropPair>();

		private int _advancedStep;

		private readonly List<int> _donationAndExchange = new List<int>();

		private int _breakDownValue;

		private readonly List<int> _jobName = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.Default), DefaultValue("")]
		public string name
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

		[ProtoMember(4, IsRequired = false, Name = "quality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this._quality = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "occupation", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int occupation
		{
			get
			{
				return this._occupation;
			}
			set
			{
				this._occupation = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(8, IsRequired = false, Name = "step", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int model
		{
			get
			{
				return this._model;
			}
			set
			{
				this._model = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "attrBaseValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attrBaseValue
		{
			get
			{
				return this._attrBaseValue;
			}
			set
			{
				this._attrBaseValue = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "attrGrowValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attrGrowValue
		{
			get
			{
				return this._attrGrowValue;
			}
			set
			{
				this._attrGrowValue = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "firstGroupId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int firstGroupId
		{
			get
			{
				return this._firstGroupId;
			}
			set
			{
				this._firstGroupId = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "frontID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frontID
		{
			get
			{
				return this._frontID;
			}
			set
			{
				this._frontID = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "offerEvoExp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int offerEvoExp
		{
			get
			{
				return this._offerEvoExp;
			}
			set
			{
				this._offerEvoExp = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "describe", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describe
		{
			get
			{
				return this._describe;
			}
			set
			{
				this._describe = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "attrNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attrNum
		{
			get
			{
				return this._attrNum;
			}
			set
			{
				this._attrNum = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "attrLibrary", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attrLibrary
		{
			get
			{
				return this._attrLibrary;
			}
			set
			{
				this._attrLibrary = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "materialIdAndNum", DataFormat = DataFormat.Default), DefaultValue("")]
		public string materialIdAndNum
		{
			get
			{
				return this._materialIdAndNum;
			}
			set
			{
				this._materialIdAndNum = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "enchantNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int enchantNum
		{
			get
			{
				return this._enchantNum;
			}
			set
			{
				this._enchantNum = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "starNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int starNum
		{
			get
			{
				return this._starNum;
			}
			set
			{
				this._starNum = value;
			}
		}

		[ProtoMember(22, Name = "boostStarAttr", DataFormat = DataFormat.TwosComplement)]
		public List<int> boostStarAttr
		{
			get
			{
				return this._boostStarAttr;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "resolveDrop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resolveDrop
		{
			get
			{
				return this._resolveDrop;
			}
			set
			{
				this._resolveDrop = value;
			}
		}

		[ProtoMember(24, Name = "smeltDrop", DataFormat = DataFormat.Default)]
		public List<zZhuangBeiPeiZhiBiao.SmeltdropPair> smeltDrop
		{
			get
			{
				return this._smeltDrop;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "advancedStep", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int advancedStep
		{
			get
			{
				return this._advancedStep;
			}
			set
			{
				this._advancedStep = value;
			}
		}

		[ProtoMember(26, Name = "donationAndExchange", DataFormat = DataFormat.TwosComplement)]
		public List<int> donationAndExchange
		{
			get
			{
				return this._donationAndExchange;
			}
		}

		[ProtoMember(27, IsRequired = false, Name = "breakDownValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int breakDownValue
		{
			get
			{
				return this._breakDownValue;
			}
			set
			{
				this._breakDownValue = value;
			}
		}

		[ProtoMember(28, Name = "jobName", DataFormat = DataFormat.TwosComplement)]
		public List<int> jobName
		{
			get
			{
				return this._jobName;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
