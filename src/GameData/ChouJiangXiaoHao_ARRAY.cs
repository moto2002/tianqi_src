using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChouJiangXiaoHao_ARRAY")]
	[Serializable]
	public class ChouJiangXiaoHao_ARRAY : IExtensible
	{
		private readonly List<ChouJiangXiaoHao> _items = new List<ChouJiangXiaoHao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChouJiangXiaoHao> items
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
