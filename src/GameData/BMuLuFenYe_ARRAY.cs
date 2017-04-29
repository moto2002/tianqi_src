using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "BMuLuFenYe_ARRAY")]
	[Serializable]
	public class BMuLuFenYe_ARRAY : IExtensible
	{
		private readonly List<BMuLuFenYe> _items = new List<BMuLuFenYe>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BMuLuFenYe> items
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
