using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TeShuChongWu_ARRAY")]
	[Serializable]
	public class TeShuChongWu_ARRAY : IExtensible
	{
		private readonly List<TeShuChongWu> _items = new List<TeShuChongWu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TeShuChongWu> items
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
