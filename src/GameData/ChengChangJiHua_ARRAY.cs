using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChengChangJiHua_ARRAY")]
	[Serializable]
	public class ChengChangJiHua_ARRAY : IExtensible
	{
		private readonly List<ChengChangJiHua> _items = new List<ChengChangJiHua>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChengChangJiHua> items
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
