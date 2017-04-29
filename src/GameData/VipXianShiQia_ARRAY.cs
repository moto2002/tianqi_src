using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "VipXianShiQia_ARRAY")]
	[Serializable]
	public class VipXianShiQia_ARRAY : IExtensible
	{
		private readonly List<VipXianShiQia> _items = new List<VipXianShiQia>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<VipXianShiQia> items
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
