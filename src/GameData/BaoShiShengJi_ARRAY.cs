using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "BaoShiShengJi_ARRAY")]
	[Serializable]
	public class BaoShiShengJi_ARRAY : IExtensible
	{
		private readonly List<BaoShiShengJi> _items = new List<BaoShiShengJi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BaoShiShengJi> items
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
