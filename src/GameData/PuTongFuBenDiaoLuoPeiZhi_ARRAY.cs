using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "PuTongFuBenDiaoLuoPeiZhi_ARRAY")]
	[Serializable]
	public class PuTongFuBenDiaoLuoPeiZhi_ARRAY : IExtensible
	{
		private readonly List<PuTongFuBenDiaoLuoPeiZhi> _items = new List<PuTongFuBenDiaoLuoPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<PuTongFuBenDiaoLuoPeiZhi> items
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
