using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DiaoLuoZu_ARRAY")]
	[Serializable]
	public class DiaoLuoZu_ARRAY : IExtensible
	{
		private readonly List<DiaoLuoZu> _items = new List<DiaoLuoZu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DiaoLuoZu> items
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
