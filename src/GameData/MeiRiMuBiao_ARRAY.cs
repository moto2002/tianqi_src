using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "MeiRiMuBiao_ARRAY")]
	[Serializable]
	public class MeiRiMuBiao_ARRAY : IExtensible
	{
		private readonly List<MeiRiMuBiao> _items = new List<MeiRiMuBiao>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<MeiRiMuBiao> items
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
