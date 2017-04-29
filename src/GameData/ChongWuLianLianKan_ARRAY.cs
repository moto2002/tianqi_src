using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChongWuLianLianKan_ARRAY")]
	[Serializable]
	public class ChongWuLianLianKan_ARRAY : IExtensible
	{
		private readonly List<ChongWuLianLianKan> _items = new List<ChongWuLianLianKan>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChongWuLianLianKan> items
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
