using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JinJiDiaoLuoPeiZhi_ARRAY")]
	[Serializable]
	public class JinJiDiaoLuoPeiZhi_ARRAY : IExtensible
	{
		private readonly List<JinJiDiaoLuoPeiZhi> _items = new List<JinJiDiaoLuoPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JinJiDiaoLuoPeiZhi> items
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
