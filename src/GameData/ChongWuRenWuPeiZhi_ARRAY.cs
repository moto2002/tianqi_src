using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChongWuRenWuPeiZhi_ARRAY")]
	[Serializable]
	public class ChongWuRenWuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<ChongWuRenWuPeiZhi> _items = new List<ChongWuRenWuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChongWuRenWuPeiZhi> items
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
