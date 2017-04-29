using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XPinZhiXiShu_ARRAY")]
	[Serializable]
	public class XPinZhiXiShu_ARRAY : IExtensible
	{
		private readonly List<XPinZhiXiShu> _items = new List<XPinZhiXiShu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XPinZhiXiShu> items
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
