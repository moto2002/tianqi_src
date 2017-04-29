using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "MonsterLibrary_ARRAY")]
	[Serializable]
	public class MonsterLibrary_ARRAY : IExtensible
	{
		private readonly List<MonsterLibrary> _items = new List<MonsterLibrary>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<MonsterLibrary> items
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
