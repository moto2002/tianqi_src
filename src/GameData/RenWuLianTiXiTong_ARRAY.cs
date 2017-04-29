using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "RenWuLianTiXiTong_ARRAY")]
	[Serializable]
	public class RenWuLianTiXiTong_ARRAY : IExtensible
	{
		private readonly List<RenWuLianTiXiTong> _items = new List<RenWuLianTiXiTong>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<RenWuLianTiXiTong> items
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
