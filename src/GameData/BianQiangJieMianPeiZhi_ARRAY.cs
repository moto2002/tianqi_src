using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "BianQiangJieMianPeiZhi_ARRAY")]
	[Serializable]
	public class BianQiangJieMianPeiZhi_ARRAY : IExtensible
	{
		private readonly List<BianQiangJieMianPeiZhi> _items = new List<BianQiangJieMianPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BianQiangJieMianPeiZhi> items
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
