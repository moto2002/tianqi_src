using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XuanShangRenWuPeiZhi_ARRAY")]
	[Serializable]
	public class XuanShangRenWuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<XuanShangRenWuPeiZhi> _items = new List<XuanShangRenWuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XuanShangRenWuPeiZhi> items
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
