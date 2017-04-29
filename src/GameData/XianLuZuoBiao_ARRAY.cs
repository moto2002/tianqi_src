using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XianLuZuoBiao_ARRAY")]
	[Serializable]
	public class XianLuZuoBiao_ARRAY : IExtensible
	{
		private readonly List<XianLuZuoBiao> _items = new List<XianLuZuoBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XianLuZuoBiao> items
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
