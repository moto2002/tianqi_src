using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhanDouXinXi_ARRAY")]
	[Serializable]
	public class ZhanDouXinXi_ARRAY : IExtensible
	{
		private readonly List<ZhanDouXinXi> _items = new List<ZhanDouXinXi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ZhanDouXinXi> items
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
