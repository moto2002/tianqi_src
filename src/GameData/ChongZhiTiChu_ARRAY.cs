using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChongZhiTiChu_ARRAY")]
	[Serializable]
	public class ChongZhiTiChu_ARRAY : IExtensible
	{
		private readonly List<ChongZhiTiChu> _items = new List<ChongZhiTiChu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChongZhiTiChu> items
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
