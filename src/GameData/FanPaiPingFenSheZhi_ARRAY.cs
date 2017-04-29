using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FanPaiPingFenSheZhi_ARRAY")]
	[Serializable]
	public class FanPaiPingFenSheZhi_ARRAY : IExtensible
	{
		private readonly List<FanPaiPingFenSheZhi> _items = new List<FanPaiPingFenSheZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FanPaiPingFenSheZhi> items
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
