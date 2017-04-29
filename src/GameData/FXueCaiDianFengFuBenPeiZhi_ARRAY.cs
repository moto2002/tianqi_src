using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FXueCaiDianFengFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class FXueCaiDianFengFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<FXueCaiDianFengFuBenPeiZhi> _items = new List<FXueCaiDianFengFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FXueCaiDianFengFuBenPeiZhi> items
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
