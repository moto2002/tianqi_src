using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DiaoLuoMoXingBiao_ARRAY")]
	[Serializable]
	public class DiaoLuoMoXingBiao_ARRAY : IExtensible
	{
		private readonly List<DiaoLuoMoXingBiao> _items = new List<DiaoLuoMoXingBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DiaoLuoMoXingBiao> items
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
