using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "zZhuangBeiQiangHua_ARRAY")]
	[Serializable]
	public class zZhuangBeiQiangHua_ARRAY : IExtensible
	{
		private readonly List<zZhuangBeiQiangHua> _items = new List<zZhuangBeiQiangHua>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<zZhuangBeiQiangHua> items
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
