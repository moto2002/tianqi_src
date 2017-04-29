using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "zTaoZhuangPeiZhi_ARRAY")]
	[Serializable]
	public class zTaoZhuangPeiZhi_ARRAY : IExtensible
	{
		private readonly List<zTaoZhuangPeiZhi> _items = new List<zTaoZhuangPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<zTaoZhuangPeiZhi> items
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
