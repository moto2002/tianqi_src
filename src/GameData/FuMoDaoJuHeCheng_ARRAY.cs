using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FuMoDaoJuHeCheng_ARRAY")]
	[Serializable]
	public class FuMoDaoJuHeCheng_ARRAY : IExtensible
	{
		private readonly List<FuMoDaoJuHeCheng> _items = new List<FuMoDaoJuHeCheng>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FuMoDaoJuHeCheng> items
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
