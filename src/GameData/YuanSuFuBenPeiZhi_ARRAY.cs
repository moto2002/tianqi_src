using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YuanSuFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class YuanSuFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<YuanSuFuBenPeiZhi> _items = new List<YuanSuFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YuanSuFuBenPeiZhi> items
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
