using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChongWuRenWuPinZhi_ARRAY")]
	[Serializable]
	public class ChongWuRenWuPinZhi_ARRAY : IExtensible
	{
		private readonly List<ChongWuRenWuPinZhi> _items = new List<ChongWuRenWuPinZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ChongWuRenWuPinZhi> items
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
