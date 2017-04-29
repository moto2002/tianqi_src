using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XBuWeiShuXingCanShu_ARRAY")]
	[Serializable]
	public class XBuWeiShuXingCanShu_ARRAY : IExtensible
	{
		private readonly List<XBuWeiShuXingCanShu> _items = new List<XBuWeiShuXingCanShu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XBuWeiShuXingCanShu> items
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
