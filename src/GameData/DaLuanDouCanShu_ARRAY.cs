using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DaLuanDouCanShu_ARRAY")]
	[Serializable]
	public class DaLuanDouCanShu_ARRAY : IExtensible
	{
		private readonly List<DaLuanDouCanShu> _items = new List<DaLuanDouCanShu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DaLuanDouCanShu> items
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
