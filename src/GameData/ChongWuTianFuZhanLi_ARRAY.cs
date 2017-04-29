using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChongWuTianFuZhanLi_ARRAY")]
	[Serializable]
	public class ChongWuTianFuZhanLi_ARRAY : IExtensible
	{
		private readonly List<ChongWuTianFuZhanLi> _items = new List<ChongWuTianFuZhanLi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChongWuTianFuZhanLi> items
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
