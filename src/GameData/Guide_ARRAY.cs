using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "Guide_ARRAY")]
	[Serializable]
	public class Guide_ARRAY : IExtensible
	{
		private readonly List<Guide> _items = new List<Guide>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<Guide> items
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
