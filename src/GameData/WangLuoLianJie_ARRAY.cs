using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "WangLuoLianJie_ARRAY")]
	[Serializable]
	public class WangLuoLianJie_ARRAY : IExtensible
	{
		private readonly List<WangLuoLianJie> _items = new List<WangLuoLianJie>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<WangLuoLianJie> items
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
