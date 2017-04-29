using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DianLiangBuShu_ARRAY")]
	[Serializable]
	public class DianLiangBuShu_ARRAY : IExtensible
	{
		private readonly List<DianLiangBuShu> _items = new List<DianLiangBuShu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DianLiangBuShu> items
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
