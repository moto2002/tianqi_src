using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GongHuiShangPinBiao_ARRAY")]
	[Serializable]
	public class GongHuiShangPinBiao_ARRAY : IExtensible
	{
		private readonly List<GongHuiShangPinBiao> _items = new List<GongHuiShangPinBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GongHuiShangPinBiao> items
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
