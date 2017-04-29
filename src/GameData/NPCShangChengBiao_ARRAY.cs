using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "NPCShangChengBiao_ARRAY")]
	[Serializable]
	public class NPCShangChengBiao_ARRAY : IExtensible
	{
		private readonly List<NPCShangChengBiao> _items = new List<NPCShangChengBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<NPCShangChengBiao> items
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
