using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TuiSongTongZhi_ARRAY")]
	[Serializable]
	public class TuiSongTongZhi_ARRAY : IExtensible
	{
		private readonly List<TuiSongTongZhi> _items = new List<TuiSongTongZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TuiSongTongZhi> items
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
