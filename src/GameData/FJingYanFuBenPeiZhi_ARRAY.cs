using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FJingYanFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class FJingYanFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<FJingYanFuBenPeiZhi> _items = new List<FJingYanFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FJingYanFuBenPeiZhi> items
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
