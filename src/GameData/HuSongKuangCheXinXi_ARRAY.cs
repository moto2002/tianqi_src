using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "HuSongKuangCheXinXi_ARRAY")]
	[Serializable]
	public class HuSongKuangCheXinXi_ARRAY : IExtensible
	{
		private readonly List<HuSongKuangCheXinXi> _items = new List<HuSongKuangCheXinXi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<HuSongKuangCheXinXi> items
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
