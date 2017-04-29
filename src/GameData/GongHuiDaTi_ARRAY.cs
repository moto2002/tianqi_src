using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GongHuiDaTi_ARRAY")]
	[Serializable]
	public class GongHuiDaTi_ARRAY : IExtensible
	{
		private readonly List<GongHuiDaTi> _items = new List<GongHuiDaTi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GongHuiDaTi> items
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
