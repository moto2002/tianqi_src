using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DongHuaShiJianBiao_ARRAY")]
	[Serializable]
	public class DongHuaShiJianBiao_ARRAY : IExtensible
	{
		private readonly List<DongHuaShiJianBiao> _items = new List<DongHuaShiJianBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DongHuaShiJianBiao> items
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
