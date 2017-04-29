using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShuaXinPinLv_ARRAY")]
	[Serializable]
	public class ShuaXinPinLv_ARRAY : IExtensible
	{
		private readonly List<ShuaXinPinLv> _items = new List<ShuaXinPinLv>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShuaXinPinLv> items
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
