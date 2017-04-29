using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "KaiShiXinXi_ARRAY")]
	[Serializable]
	public class KaiShiXinXi_ARRAY : IExtensible
	{
		private readonly List<KaiShiXinXi> _items = new List<KaiShiXinXi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<KaiShiXinXi> items
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
