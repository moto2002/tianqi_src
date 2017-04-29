using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GouMaiXiangGuan_ARRAY")]
	[Serializable]
	public class GouMaiXiangGuan_ARRAY : IExtensible
	{
		private readonly List<GouMaiXiangGuan> _items = new List<GouMaiXiangGuan>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GouMaiXiangGuan> items
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
