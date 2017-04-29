using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "XuanShangRenWuXuLie_ARRAY")]
	[Serializable]
	public class XuanShangRenWuXuLie_ARRAY : IExtensible
	{
		private readonly List<XuanShangRenWuXuLie> _items = new List<XuanShangRenWuXuLie>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<XuanShangRenWuXuLie> items
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
