using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShiJianCanShuBiao_ARRAY")]
	[Serializable]
	public class ShiJianCanShuBiao_ARRAY : IExtensible
	{
		private readonly List<ShiJianCanShuBiao> _items = new List<ShiJianCanShuBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShiJianCanShuBiao> items
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
