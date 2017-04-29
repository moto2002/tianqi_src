using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "GouMaiCiShu_ARRAY")]
	[Serializable]
	public class GouMaiCiShu_ARRAY : IExtensible
	{
		private readonly List<GouMaiCiShu> _items = new List<GouMaiCiShu>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<GouMaiCiShu> items
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
