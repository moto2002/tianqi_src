using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShengXingFanHaiPeiZhi_ARRAY")]
	[Serializable]
	public class ShengXingFanHaiPeiZhi_ARRAY : IExtensible
	{
		private readonly List<ShengXingFanHaiPeiZhi> _items = new List<ShengXingFanHaiPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShengXingFanHaiPeiZhi> items
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
