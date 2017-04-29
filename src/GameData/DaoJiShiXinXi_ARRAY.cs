using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DaoJiShiXinXi_ARRAY")]
	[Serializable]
	public class DaoJiShiXinXi_ARRAY : IExtensible
	{
		private readonly List<DaoJiShiXinXi> _items = new List<DaoJiShiXinXi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<DaoJiShiXinXi> items
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
