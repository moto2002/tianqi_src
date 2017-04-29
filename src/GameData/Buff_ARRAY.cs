using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "Buff_ARRAY")]
	[Serializable]
	public class Buff_ARRAY : IExtensible
	{
		private readonly List<Buff> _items = new List<Buff>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<Buff> items
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
