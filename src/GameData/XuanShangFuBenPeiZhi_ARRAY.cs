using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XuanShangFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class XuanShangFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<XuanShangFuBenPeiZhi> _items = new List<XuanShangFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XuanShangFuBenPeiZhi> items
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
