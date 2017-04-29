using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FShouHuShuiJingFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class FShouHuShuiJingFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<FShouHuShuiJingFuBenPeiZhi> _items = new List<FShouHuShuiJingFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FShouHuShuiJingFuBenPeiZhi> items
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
