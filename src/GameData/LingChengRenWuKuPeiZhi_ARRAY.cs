using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "LingChengRenWuKuPeiZhi_ARRAY")]
	[Serializable]
	public class LingChengRenWuKuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<LingChengRenWuKuPeiZhi> _items = new List<LingChengRenWuKuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<LingChengRenWuKuPeiZhi> items
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
