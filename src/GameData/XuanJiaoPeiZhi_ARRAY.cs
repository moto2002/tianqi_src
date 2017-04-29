using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XuanJiaoPeiZhi_ARRAY")]
	[Serializable]
	public class XuanJiaoPeiZhi_ARRAY : IExtensible
	{
		private readonly List<XuanJiaoPeiZhi> _items = new List<XuanJiaoPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XuanJiaoPeiZhi> items
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
