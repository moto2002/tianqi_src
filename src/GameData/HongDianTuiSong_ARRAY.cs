using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "HongDianTuiSong_ARRAY")]
	[Serializable]
	public class HongDianTuiSong_ARRAY : IExtensible
	{
		private readonly List<HongDianTuiSong> _items = new List<HongDianTuiSong>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<HongDianTuiSong> items
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
