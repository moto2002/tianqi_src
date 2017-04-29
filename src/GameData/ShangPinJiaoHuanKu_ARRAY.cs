using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ShangPinJiaoHuanKu_ARRAY")]
	[Serializable]
	public class ShangPinJiaoHuanKu_ARRAY : IExtensible
	{
		private readonly List<ShangPinJiaoHuanKu> _items = new List<ShangPinJiaoHuanKu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ShangPinJiaoHuanKu> items
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
