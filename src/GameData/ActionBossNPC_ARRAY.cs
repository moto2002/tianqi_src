using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ActionBossNPC_ARRAY")]
	[Serializable]
	public class ActionBossNPC_ARRAY : IExtensible
	{
		private readonly List<ActionBossNPC> _items = new List<ActionBossNPC>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ActionBossNPC> items
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
