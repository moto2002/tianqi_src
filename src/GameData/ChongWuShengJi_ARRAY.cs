using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChongWuShengJi_ARRAY")]
	[Serializable]
	public class ChongWuShengJi_ARRAY : IExtensible
	{
		private readonly List<ChongWuShengJi> _items = new List<ChongWuShengJi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChongWuShengJi> items
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
