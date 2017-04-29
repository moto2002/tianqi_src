using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JinBiGouMai_ARRAY")]
	[Serializable]
	public class JinBiGouMai_ARRAY : IExtensible
	{
		private readonly List<JinBiGouMai> _items = new List<JinBiGouMai>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JinBiGouMai> items
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
