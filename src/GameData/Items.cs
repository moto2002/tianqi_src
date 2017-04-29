using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Items")]
	[Serializable]
	public class Items : IExtensible
	{
		[ProtoContract(Name = "SellpricePair")]
		[Serializable]
		public class SellpricePair : IExtensible
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

		[ProtoContract(Name = "OriginalpricePair")]
		[Serializable]
		public class OriginalpricePair : IExtensible
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

		[ProtoContract(Name = "DiscountedpricePair")]
		[Serializable]
		public class DiscountedpricePair : IExtensible
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

		private int _name;

		private int _firstType;

		private int _secondType;

		private int _level;

		private int _career;

		private int _minLv;

		private int _color;

		private int _icon;

		private int _littleIcon;

		private int _overlay;

		private int _describeId1;

		private int _describeId2;

		private int _function;

		private int _effectId;

		private int _tab;

		private int _order;

		private readonly List<int> _getType = new List<int>();

		private int _position;

		private int _model;

		private int _modelId;

		private int _atti;

		private int _point;

		private int _show;

		private readonly List<int> _getWay = new List<int>();

		private int _fragment;

		private int _promptWay;

		private int _promptId;

		private int _step;

		private readonly List<Items.SellpricePair> _sellPrice = new List<Items.SellpricePair>();

		private readonly List<Items.OriginalpricePair> _originalPrice = new List<Items.OriginalpricePair>();

		private readonly List<Items.DiscountedpricePair> _discountedPrice = new List<Items.DiscountedpricePair>();

		private float _time;

		private string _fashionId = string.Empty;

		private int _fashionBuff;

		private int _gogok;

		private int _effectRank;

		private int _deathDrop;

		private int _useTips;

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

		[ProtoMember(3, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "firstType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int firstType
		{
			get
			{
				return this._firstType;
			}
			set
			{
				this._firstType = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "secondType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int secondType
		{
			get
			{
				return this._secondType;
			}
			set
			{
				this._secondType = value;
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

		[ProtoMember(8, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "color", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(12, IsRequired = false, Name = "littleIcon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int littleIcon
		{
			get
			{
				return this._littleIcon;
			}
			set
			{
				this._littleIcon = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "overlay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int overlay
		{
			get
			{
				return this._overlay;
			}
			set
			{
				this._overlay = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "describeId1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describeId1
		{
			get
			{
				return this._describeId1;
			}
			set
			{
				this._describeId1 = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "describeId2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describeId2
		{
			get
			{
				return this._describeId2;
			}
			set
			{
				this._describeId2 = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "function", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int function
		{
			get
			{
				return this._function;
			}
			set
			{
				this._function = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "effectId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "tab", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tab
		{
			get
			{
				return this._tab;
			}
			set
			{
				this._tab = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "order", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		[ProtoMember(20, Name = "getType", DataFormat = DataFormat.TwosComplement)]
		public List<int> getType
		{
			get
			{
				return this._getType;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(22, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(23, IsRequired = false, Name = "modelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "atti", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int atti
		{
			get
			{
				return this._atti;
			}
			set
			{
				this._atti = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "point", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int point
		{
			get
			{
				return this._point;
			}
			set
			{
				this._point = value;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "show", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int show
		{
			get
			{
				return this._show;
			}
			set
			{
				this._show = value;
			}
		}

		[ProtoMember(27, Name = "getWay", DataFormat = DataFormat.TwosComplement)]
		public List<int> getWay
		{
			get
			{
				return this._getWay;
			}
		}

		[ProtoMember(28, IsRequired = false, Name = "fragment", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fragment
		{
			get
			{
				return this._fragment;
			}
			set
			{
				this._fragment = value;
			}
		}

		[ProtoMember(29, IsRequired = false, Name = "promptWay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int promptWay
		{
			get
			{
				return this._promptWay;
			}
			set
			{
				this._promptWay = value;
			}
		}

		[ProtoMember(30, IsRequired = false, Name = "promptId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int promptId
		{
			get
			{
				return this._promptId;
			}
			set
			{
				this._promptId = value;
			}
		}

		[ProtoMember(31, IsRequired = false, Name = "step", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(32, Name = "sellPrice", DataFormat = DataFormat.Default)]
		public List<Items.SellpricePair> sellPrice
		{
			get
			{
				return this._sellPrice;
			}
		}

		[ProtoMember(33, Name = "originalPrice", DataFormat = DataFormat.Default)]
		public List<Items.OriginalpricePair> originalPrice
		{
			get
			{
				return this._originalPrice;
			}
		}

		[ProtoMember(34, Name = "discountedPrice", DataFormat = DataFormat.Default)]
		public List<Items.DiscountedpricePair> discountedPrice
		{
			get
			{
				return this._discountedPrice;
			}
		}

		[ProtoMember(35, IsRequired = false, Name = "time", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float time
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

		[ProtoMember(36, IsRequired = false, Name = "fashionId", DataFormat = DataFormat.Default), DefaultValue("")]
		public string fashionId
		{
			get
			{
				return this._fashionId;
			}
			set
			{
				this._fashionId = value;
			}
		}

		[ProtoMember(37, IsRequired = false, Name = "fashionBuff", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fashionBuff
		{
			get
			{
				return this._fashionBuff;
			}
			set
			{
				this._fashionBuff = value;
			}
		}

		[ProtoMember(38, IsRequired = false, Name = "gogok", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gogok
		{
			get
			{
				return this._gogok;
			}
			set
			{
				this._gogok = value;
			}
		}

		[ProtoMember(39, IsRequired = false, Name = "effectRank", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effectRank
		{
			get
			{
				return this._effectRank;
			}
			set
			{
				this._effectRank = value;
			}
		}

		[ProtoMember(40, IsRequired = false, Name = "deathDrop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deathDrop
		{
			get
			{
				return this._deathDrop;
			}
			set
			{
				this._deathDrop = value;
			}
		}

		[ProtoMember(41, IsRequired = false, Name = "useTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int useTips
		{
			get
			{
				return this._useTips;
			}
			set
			{
				this._useTips = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
