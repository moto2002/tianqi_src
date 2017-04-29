using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhuXianZhangJiePeiZhi_ARRAY")]
	[Serializable]
	public class ZhuXianZhangJiePeiZhi_ARRAY : IExtensible
	{
		private readonly List<ZhuXianZhangJiePeiZhi> _items = new List<ZhuXianZhangJiePeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhuXianZhangJiePeiZhi> items
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
