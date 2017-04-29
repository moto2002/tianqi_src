using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhuoYueShuXing_ARRAY")]
	[Serializable]
	public class ZhuoYueShuXing_ARRAY : IExtensible
	{
		private readonly List<ZhuoYueShuXing> _items = new List<ZhuoYueShuXing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhuoYueShuXing> items
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
