using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XuanShangJiFenDuan_ARRAY")]
	[Serializable]
	public class XuanShangJiFenDuan_ARRAY : IExtensible
	{
		private readonly List<XuanShangJiFenDuan> _items = new List<XuanShangJiFenDuan>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XuanShangJiFenDuan> items
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
