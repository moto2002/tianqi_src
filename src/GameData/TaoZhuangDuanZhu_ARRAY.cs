using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TaoZhuangDuanZhu_ARRAY")]
	[Serializable]
	public class TaoZhuangDuanZhu_ARRAY : IExtensible
	{
		private readonly List<TaoZhuangDuanZhu> _items = new List<TaoZhuangDuanZhu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TaoZhuangDuanZhu> items
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
