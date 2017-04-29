using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XianLuShengCheng_ARRAY")]
	[Serializable]
	public class XianLuShengCheng_ARRAY : IExtensible
	{
		private readonly List<XianLuShengCheng> _items = new List<XianLuShengCheng>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XianLuShengCheng> items
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
