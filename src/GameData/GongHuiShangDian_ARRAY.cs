using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GongHuiShangDian_ARRAY")]
	[Serializable]
	public class GongHuiShangDian_ARRAY : IExtensible
	{
		private readonly List<GongHuiShangDian> _items = new List<GongHuiShangDian>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GongHuiShangDian> items
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
