using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "PaoHuanRenWuPeiZhi_ARRAY")]
	[Serializable]
	public class PaoHuanRenWuPeiZhi_ARRAY : IExtensible
	{
		private readonly List<PaoHuanRenWuPeiZhi> _items = new List<PaoHuanRenWuPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<PaoHuanRenWuPeiZhi> items
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
