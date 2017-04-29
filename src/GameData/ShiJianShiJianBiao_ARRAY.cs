using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShiJianShiJianBiao_ARRAY")]
	[Serializable]
	public class ShiJianShiJianBiao_ARRAY : IExtensible
	{
		private readonly List<ShiJianShiJianBiao> _items = new List<ShiJianShiJianBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShiJianShiJianBiao> items
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
