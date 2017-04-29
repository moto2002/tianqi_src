using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(636), ForSend(636), ProtoContract(Name = "NovicePacksInfo")]
	[Serializable]
	public class NovicePacksInfo : IExtensible
	{
		public static readonly short OP = 636;

		private int _id;

		private readonly List<DropItem> _items = new List<DropItem>();

		private int _price;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, Name = "items", DataFormat = DataFormat.Default)]
		public List<DropItem> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "price", DataFormat = DataFormat.TwosComplement)]
		public int price
		{
			get
			{
				return this._price;
			}
			set
			{
				this._price = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
