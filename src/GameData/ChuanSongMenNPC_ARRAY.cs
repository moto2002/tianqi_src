using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChuanSongMenNPC_ARRAY")]
	[Serializable]
	public class ChuanSongMenNPC_ARRAY : IExtensible
	{
		private readonly List<ChuanSongMenNPC> _items = new List<ChuanSongMenNPC>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChuanSongMenNPC> items
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
