using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShiZhuangXiTong_ARRAY")]
	[Serializable]
	public class ShiZhuangXiTong_ARRAY : IExtensible
	{
		private readonly List<ShiZhuangXiTong> _items = new List<ShiZhuangXiTong>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShiZhuangXiTong> items
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
