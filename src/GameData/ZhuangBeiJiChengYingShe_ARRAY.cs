using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhuangBeiJiChengYingShe_ARRAY")]
	[Serializable]
	public class ZhuangBeiJiChengYingShe_ARRAY : IExtensible
	{
		private readonly List<ZhuangBeiJiChengYingShe> _items = new List<ZhuangBeiJiChengYingShe>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhuangBeiJiChengYingShe> items
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
