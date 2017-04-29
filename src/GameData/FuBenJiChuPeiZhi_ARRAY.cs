using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FuBenJiChuPeiZhi_ARRAY")]
	[Serializable]
	public class FuBenJiChuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<FuBenJiChuPeiZhi> _items = new List<FuBenJiChuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FuBenJiChuPeiZhi> items
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
