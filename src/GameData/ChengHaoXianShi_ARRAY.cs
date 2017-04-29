using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChengHaoXianShi_ARRAY")]
	[Serializable]
	public class ChengHaoXianShi_ARRAY : IExtensible
	{
		private readonly List<ChengHaoXianShi> _items = new List<ChengHaoXianShi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChengHaoXianShi> items
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
