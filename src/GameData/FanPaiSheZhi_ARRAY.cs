using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FanPaiSheZhi_ARRAY")]
	[Serializable]
	public class FanPaiSheZhi_ARRAY : IExtensible
	{
		private readonly List<FanPaiSheZhi> _items = new List<FanPaiSheZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FanPaiSheZhi> items
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
