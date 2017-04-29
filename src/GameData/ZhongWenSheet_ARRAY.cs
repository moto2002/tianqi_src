using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhongWenSheet_ARRAY")]
	[Serializable]
	public class ZhongWenSheet_ARRAY : IExtensible
	{
		private readonly List<ZhongWenSheet> _items = new List<ZhongWenSheet>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhongWenSheet> items
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
