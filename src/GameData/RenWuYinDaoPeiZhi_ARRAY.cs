using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "RenWuYinDaoPeiZhi_ARRAY")]
	[Serializable]
	public class RenWuYinDaoPeiZhi_ARRAY : IExtensible
	{
		private readonly List<RenWuYinDaoPeiZhi> _items = new List<RenWuYinDaoPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<RenWuYinDaoPeiZhi> items
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
