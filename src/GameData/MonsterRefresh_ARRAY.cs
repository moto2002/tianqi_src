using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "MonsterRefresh_ARRAY")]
	[Serializable]
	public class MonsterRefresh_ARRAY : IExtensible
	{
		private readonly List<MonsterRefresh> _items = new List<MonsterRefresh>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<MonsterRefresh> items
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
