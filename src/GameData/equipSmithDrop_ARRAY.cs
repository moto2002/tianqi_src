using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "equipSmithDrop_ARRAY")]
	[Serializable]
	public class equipSmithDrop_ARRAY : IExtensible
	{
		private readonly List<equipSmithDrop> _items = new List<equipSmithDrop>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<equipSmithDrop> items
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
