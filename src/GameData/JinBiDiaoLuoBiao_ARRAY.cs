using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "JinBiDiaoLuoBiao_ARRAY")]
	[Serializable]
	public class JinBiDiaoLuoBiao_ARRAY : IExtensible
	{
		private readonly List<JinBiDiaoLuoBiao> _items = new List<JinBiDiaoLuoBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<JinBiDiaoLuoBiao> items
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
