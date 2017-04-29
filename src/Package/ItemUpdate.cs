using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ItemUpdate")]
	[Serializable]
	public class ItemUpdate : IExtensible
	{
		private ItemOperation.IO _type;

		private readonly List<ItemInfo> _items = new List<ItemInfo>();

		private BagType.BT _bagType;

		private int _capacity;

		private int _size;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public ItemOperation.IO type
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

		[ProtoMember(2, Name = "items", DataFormat = DataFormat.Default)]
		public List<ItemInfo> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "bagType", DataFormat = DataFormat.TwosComplement), DefaultValue(BagType.BT.Bag)]
		public BagType.BT bagType
		{
			get
			{
				return this._bagType;
			}
			set
			{
				this._bagType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "capacity", DataFormat = DataFormat.ZigZag), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "size", DataFormat = DataFormat.ZigZag), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
