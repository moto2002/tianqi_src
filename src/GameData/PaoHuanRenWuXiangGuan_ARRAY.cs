using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "PaoHuanRenWuXiangGuan_ARRAY")]
	[Serializable]
	public class PaoHuanRenWuXiangGuan_ARRAY : IExtensible
	{
		private readonly List<PaoHuanRenWuXiangGuan> _items = new List<PaoHuanRenWuXiangGuan>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<PaoHuanRenWuXiangGuan> items
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
