using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "zZhuangBeiPeiZhiBiao_ARRAY")]
	[Serializable]
	public class zZhuangBeiPeiZhiBiao_ARRAY : IExtensible
	{
		private readonly List<zZhuangBeiPeiZhiBiao> _items = new List<zZhuangBeiPeiZhiBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<zZhuangBeiPeiZhiBiao> items
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
