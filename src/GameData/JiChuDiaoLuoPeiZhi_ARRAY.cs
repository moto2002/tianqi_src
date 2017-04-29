using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JiChuDiaoLuoPeiZhi_ARRAY")]
	[Serializable]
	public class JiChuDiaoLuoPeiZhi_ARRAY : IExtensible
	{
		private readonly List<JiChuDiaoLuoPeiZhi> _items = new List<JiChuDiaoLuoPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JiChuDiaoLuoPeiZhi> items
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
