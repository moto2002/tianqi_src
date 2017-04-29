using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TiLiGouMai_ARRAY")]
	[Serializable]
	public class TiLiGouMai_ARRAY : IExtensible
	{
		private readonly List<TiLiGouMai> _items = new List<TiLiGouMai>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TiLiGouMai> items
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
