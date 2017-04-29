using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DLuJingShuXing_ARRAY")]
	[Serializable]
	public class DLuJingShuXing_ARRAY : IExtensible
	{
		private readonly List<DLuJingShuXing> _items = new List<DLuJingShuXing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DLuJingShuXing> items
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
