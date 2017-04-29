using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShengLiBaoXiang_ARRAY")]
	[Serializable]
	public class ShengLiBaoXiang_ARRAY : IExtensible
	{
		private readonly List<ShengLiBaoXiang> _items = new List<ShengLiBaoXiang>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShengLiBaoXiang> items
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
