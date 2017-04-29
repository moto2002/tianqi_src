using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChuShiXiangJi_ARRAY")]
	[Serializable]
	public class ChuShiXiangJi_ARRAY : IExtensible
	{
		private readonly List<ChuShiXiangJi> _items = new List<ChuShiXiangJi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChuShiXiangJi> items
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
