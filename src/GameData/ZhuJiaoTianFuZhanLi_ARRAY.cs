using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhuJiaoTianFuZhanLi_ARRAY")]
	[Serializable]
	public class ZhuJiaoTianFuZhanLi_ARRAY : IExtensible
	{
		private readonly List<ZhuJiaoTianFuZhanLi> _items = new List<ZhuJiaoTianFuZhanLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhuJiaoTianFuZhanLi> items
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
