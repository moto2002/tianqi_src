using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShiTiPengZhuangBiao_ARRAY")]
	[Serializable]
	public class ShiTiPengZhuangBiao_ARRAY : IExtensible
	{
		private readonly List<ShiTiPengZhuangBiao> _items = new List<ShiTiPengZhuangBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShiTiPengZhuangBiao> items
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
