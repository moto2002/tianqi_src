using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhuJiaoJiNengZhanLi_ARRAY")]
	[Serializable]
	public class ZhuJiaoJiNengZhanLi_ARRAY : IExtensible
	{
		private readonly List<ZhuJiaoJiNengZhanLi> _items = new List<ZhuJiaoJiNengZhanLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhuJiaoJiNengZhanLi> items
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
