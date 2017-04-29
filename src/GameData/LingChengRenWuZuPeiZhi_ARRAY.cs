using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "LingChengRenWuZuPeiZhi_ARRAY")]
	[Serializable]
	public class LingChengRenWuZuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<LingChengRenWuZuPeiZhi> _items = new List<LingChengRenWuZuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<LingChengRenWuZuPeiZhi> items
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
