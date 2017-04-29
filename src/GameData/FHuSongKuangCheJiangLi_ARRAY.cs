using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FHuSongKuangCheJiangLi_ARRAY")]
	[Serializable]
	public class FHuSongKuangCheJiangLi_ARRAY : IExtensible
	{
		private readonly List<FHuSongKuangCheJiangLi> _items = new List<FHuSongKuangCheJiangLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FHuSongKuangCheJiangLi> items
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
