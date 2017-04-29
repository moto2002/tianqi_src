using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShengXingCaiLiaoHeCheng_ARRAY")]
	[Serializable]
	public class ShengXingCaiLiaoHeCheng_ARRAY : IExtensible
	{
		private readonly List<ShengXingCaiLiaoHeCheng> _items = new List<ShengXingCaiLiaoHeCheng>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShengXingCaiLiaoHeCheng> items
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
