using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "SCaiLiaoTiGongJingYanZhi_ARRAY")]
	[Serializable]
	public class SCaiLiaoTiGongJingYanZhi_ARRAY : IExtensible
	{
		private readonly List<SCaiLiaoTiGongJingYanZhi> _items = new List<SCaiLiaoTiGongJingYanZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<SCaiLiaoTiGongJingYanZhi> items
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
