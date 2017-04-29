using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "RiChangJiangLiMiaoShu_ARRAY")]
	[Serializable]
	public class RiChangJiangLiMiaoShu_ARRAY : IExtensible
	{
		private readonly List<RiChangJiangLiMiaoShu> _items = new List<RiChangJiangLiMiaoShu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<RiChangJiangLiMiaoShu> items
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
