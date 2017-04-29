using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "Bag")]
	[Serializable]
	public class Bag : IExtensible
	{
		private BagType.BT _type;

		private int _capacity;

		private int _size;

		private ItemInfos _items;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public BagType.BT type
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

		[ProtoMember(2, IsRequired = true, Name = "capacity", DataFormat = DataFormat.ZigZag)]
		public int capacity
		{
			get
			{
				return this._capacity;
			}
			set
			{
				this._capacity = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "size", DataFormat = DataFormat.ZigZag)]
		public int size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "items", DataFormat = DataFormat.Default), DefaultValue(null)]
		public ItemInfos items
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
