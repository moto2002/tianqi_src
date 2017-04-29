using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FXueCaiDianFengFuBenBoCi_ARRAY")]
	[Serializable]
	public class FXueCaiDianFengFuBenBoCi_ARRAY : IExtensible
	{
		private readonly List<FXueCaiDianFengFuBenBoCi> _items = new List<FXueCaiDianFengFuBenBoCi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FXueCaiDianFengFuBenBoCi> items
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
