using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GongHuiShangDianShuaXin_ARRAY")]
	[Serializable]
	public class GongHuiShangDianShuaXin_ARRAY : IExtensible
	{
		private readonly List<GongHuiShangDianShuaXin> _items = new List<GongHuiShangDianShuaXin>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GongHuiShangDianShuaXin> items
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
