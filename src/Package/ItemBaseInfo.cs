using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ItemBaseInfo")]
	[Serializable]
	public class ItemBaseInfo : IExtensible
	{
		private long _id;

		private int _itemId;

		private int _quality;

		private int _index;

		private int _count;

		private int _bagType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
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

		[ProtoMember(2, IsRequired = true, Name = "itemId", DataFormat = DataFormat.ZigZag)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "quality", DataFormat = DataFormat.ZigZag)]
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

		[ProtoMember(4, IsRequired = true, Name = "index", DataFormat = DataFormat.ZigZag)]
		public int index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "count", DataFormat = DataFormat.ZigZag)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "bagType", DataFormat = DataFormat.ZigZag)]
		public int bagType
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
