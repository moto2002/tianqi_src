using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShengXingJiChuPeiZhi_ARRAY")]
	[Serializable]
	public class ShengXingJiChuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<ShengXingJiChuPeiZhi> _items = new List<ShengXingJiChuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShengXingJiChuPeiZhi> items
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
