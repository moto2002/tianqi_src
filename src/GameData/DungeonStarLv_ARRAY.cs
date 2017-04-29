using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DungeonStarLv_ARRAY")]
	[Serializable]
	public class DungeonStarLv_ARRAY : IExtensible
	{
		private readonly List<DungeonStarLv> _items = new List<DungeonStarLv>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DungeonStarLv> items
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
