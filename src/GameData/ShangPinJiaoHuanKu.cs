using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShangPinJiaoHuanKu")]
	[Serializable]
	public class ShangPinJiaoHuanKu : IExtensible
	{
		[ProtoContract(Name = "ItemidPair")]
		[Serializable]
		public class ItemidPair : IExtensible
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

		private int _Id;

		private int _libraryId;

		private int _type;

		private readonly List<ShangPinJiaoHuanKu.ItemidPair> _itemId = new List<ShangPinJiaoHuanKu.ItemidPair>();

		private string _equipData = string.Empty;

		private int _weight;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "libraryId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int libraryId
		{
			get
			{
				return this._libraryId;
			}
			set
			{
				this._libraryId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(5, Name = "itemId", DataFormat = DataFormat.Default)]
		public List<ShangPinJiaoHuanKu.ItemidPair> itemId
		{
			get
			{
				return this._itemId;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "equipData", DataFormat = DataFormat.Default), DefaultValue("")]
		public string equipData
		{
			get
			{
				return this._equipData;
			}
			set
			{
				this._equipData = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
