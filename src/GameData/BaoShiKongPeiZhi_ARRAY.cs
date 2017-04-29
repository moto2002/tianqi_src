using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "BaoShiKongPeiZhi_ARRAY")]
	[Serializable]
	public class BaoShiKongPeiZhi_ARRAY : IExtensible
	{
		private readonly List<BaoShiKongPeiZhi> _items = new List<BaoShiKongPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BaoShiKongPeiZhi> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
