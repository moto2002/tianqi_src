using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhuoYueShuXingKu_ARRAY")]
	[Serializable]
	public class ZhuoYueShuXingKu_ARRAY : IExtensible
	{
		private readonly List<ZhuoYueShuXingKu> _items = new List<ZhuoYueShuXingKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhuoYueShuXingKu> items
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
