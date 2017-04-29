using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JiGuanShiJianBiao_ARRAY")]
	[Serializable]
	public class JiGuanShiJianBiao_ARRAY : IExtensible
	{
		private readonly List<JiGuanShiJianBiao> _items = new List<JiGuanShiJianBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JiGuanShiJianBiao> items
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
