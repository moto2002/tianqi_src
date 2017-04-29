using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FHuSongKuangCheFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class FHuSongKuangCheFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<FHuSongKuangCheFuBenPeiZhi> _items = new List<FHuSongKuangCheFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FHuSongKuangCheFuBenPeiZhi> items
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
