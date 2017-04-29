using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TaoZhuangXiangGuanXiShu_ARRAY")]
	[Serializable]
	public class TaoZhuangXiangGuanXiShu_ARRAY : IExtensible
	{
		private readonly List<TaoZhuangXiangGuanXiShu> _items = new List<TaoZhuangXiangGuanXiShu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TaoZhuangXiangGuanXiShu> items
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
