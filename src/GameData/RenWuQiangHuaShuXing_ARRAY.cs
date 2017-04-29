using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "RenWuQiangHuaShuXing_ARRAY")]
	[Serializable]
	public class RenWuQiangHuaShuXing_ARRAY : IExtensible
	{
		private readonly List<RenWuQiangHuaShuXing> _items = new List<RenWuQiangHuaShuXing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<RenWuQiangHuaShuXing> items
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
