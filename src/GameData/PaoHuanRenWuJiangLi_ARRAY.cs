using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "PaoHuanRenWuJiangLi_ARRAY")]
	[Serializable]
	public class PaoHuanRenWuJiangLi_ARRAY : IExtensible
	{
		private readonly List<PaoHuanRenWuJiangLi> _items = new List<PaoHuanRenWuJiangLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<PaoHuanRenWuJiangLi> items
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
