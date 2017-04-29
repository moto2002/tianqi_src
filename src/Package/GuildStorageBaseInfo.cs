using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GuildStorageBaseInfo")]
	[Serializable]
	public class GuildStorageBaseInfo : IExtensible
	{
		private long _guildId;

		private int _capacity;

		private int _size;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

		private int _exchanges;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "guildId", DataFormat = DataFormat.TwosComplement)]
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this._guildId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "capacity", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = true, Name = "size", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, Name = "items", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "exchanges", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int exchanges
		{
			get
			{
				return this._exchanges;
			}
			set
			{
				this._exchanges = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
