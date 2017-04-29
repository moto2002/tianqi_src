using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "VipXiaoGuo_ARRAY")]
	[Serializable]
	public class VipXiaoGuo_ARRAY : IExtensible
	{
		private readonly List<VipXiaoGuo> _items = new List<VipXiaoGuo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<VipXiaoGuo> items
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
