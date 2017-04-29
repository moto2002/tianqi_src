using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhuanZhiRenWu_ARRAY")]
	[Serializable]
	public class ZhuanZhiRenWu_ARRAY : IExtensible
	{
		private readonly List<ZhuanZhiRenWu> _items = new List<ZhuanZhiRenWu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhuanZhiRenWu> items
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
