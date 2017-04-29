using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChongZhiFangShi")]
	[Serializable]
	public class ChongZhiFangShi : IExtensible
	{
		[ProtoContract(Name = "DropidPair")]
		[Serializable]
		public class DropidPair : IExtensible
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

		private int _indexes;

		private int _open;

		private int _ID;

		private int _result;

		private int _order;

		private int _channel;

		private float _rmb;

		private readonly List<ChongZhiFangShi.DropidPair> _dropID = new List<ChongZhiFangShi.DropidPair>();

		private int _diamonds;

		private int _vip;

		private int _activity;

		private int _addDiamonds;

		private int _firstDiamonds;

		private int _diamondsIcon;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "indexes", DataFormat = DataFormat.TwosComplement)]
		public int indexes
		{
			get
			{
				return this._indexes;
			}
			set
			{
				this._indexes = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "open", DataFormat = DataFormat.TwosComplement)]
		public int open
		{
			get
			{
				return this._open;
			}
			set
			{
				this._open = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "result", DataFormat = DataFormat.TwosComplement)]
		public int result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "order", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(7, IsRequired = true, Name = "channel", DataFormat = DataFormat.TwosComplement)]
		public int channel
		{
			get
			{
				return this._channel;
			}
			set
			{
				this._channel = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "rmb", DataFormat = DataFormat.FixedSize)]
		public float rmb
		{
			get
			{
				return this._rmb;
			}
			set
			{
				this._rmb = value;
			}
		}

		[ProtoMember(9, Name = "dropID", DataFormat = DataFormat.Default)]
		public List<ChongZhiFangShi.DropidPair> dropID
		{
			get
			{
				return this._dropID;
			}
		}

		[ProtoMember(10, IsRequired = true, Name = "diamonds", DataFormat = DataFormat.TwosComplement)]
		public int diamonds
		{
			get
			{
				return this._diamonds;
			}
			set
			{
				this._diamonds = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "vip", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vip
		{
			get
			{
				return this._vip;
			}
			set
			{
				this._vip = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "activity", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activity
		{
			get
			{
				return this._activity;
			}
			set
			{
				this._activity = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "addDiamonds", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int addDiamonds
		{
			get
			{
				return this._addDiamonds;
			}
			set
			{
				this._addDiamonds = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "firstDiamonds", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int firstDiamonds
		{
			get
			{
				return this._firstDiamonds;
			}
			set
			{
				this._firstDiamonds = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "diamondsIcon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int diamondsIcon
		{
			get
			{
				return this._diamondsIcon;
			}
			set
			{
				this._diamondsIcon = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
