using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhuanZhiJiChuPeiZhi_ARRAY")]
	[Serializable]
	public class ZhuanZhiJiChuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<ZhuanZhiJiChuPeiZhi> _items = new List<ZhuanZhiJiChuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhuanZhiJiChuPeiZhi> items
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
