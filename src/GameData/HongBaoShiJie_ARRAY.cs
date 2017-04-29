using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "HongBaoShiJie_ARRAY")]
	[Serializable]
	public class HongBaoShiJie_ARRAY : IExtensible
	{
		private readonly List<HongBaoShiJie> _items = new List<HongBaoShiJie>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<HongBaoShiJie> items
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
