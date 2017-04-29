using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "zBuWeiQiangHua_ARRAY")]
	[Serializable]
	public class zBuWeiQiangHua_ARRAY : IExtensible
	{
		private readonly List<zBuWeiQiangHua> _items = new List<zBuWeiQiangHua>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<zBuWeiQiangHua> items
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
