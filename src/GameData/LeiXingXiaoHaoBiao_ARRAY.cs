using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "LeiXingXiaoHaoBiao_ARRAY")]
	[Serializable]
	public class LeiXingXiaoHaoBiao_ARRAY : IExtensible
	{
		private readonly List<LeiXingXiaoHaoBiao> _items = new List<LeiXingXiaoHaoBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<LeiXingXiaoHaoBiao> items
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
