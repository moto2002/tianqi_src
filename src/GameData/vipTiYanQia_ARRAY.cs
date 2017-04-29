using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "vipTiYanQia_ARRAY")]
	[Serializable]
	public class vipTiYanQia_ARRAY : IExtensible
	{
		private readonly List<vipTiYanQia> _items = new List<vipTiYanQia>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<vipTiYanQia> items
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
