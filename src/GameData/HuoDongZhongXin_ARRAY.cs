using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "HuoDongZhongXin_ARRAY")]
	[Serializable]
	public class HuoDongZhongXin_ARRAY : IExtensible
	{
		private readonly List<HuoDongZhongXin> _items = new List<HuoDongZhongXin>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<HuoDongZhongXin> items
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
