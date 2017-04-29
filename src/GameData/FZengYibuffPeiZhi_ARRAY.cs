using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "FZengYibuffPeiZhi_ARRAY")]
	[Serializable]
	public class FZengYibuffPeiZhi_ARRAY : IExtensible
	{
		private readonly List<FZengYibuffPeiZhi> _items = new List<FZengYibuffPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FZengYibuffPeiZhi> items
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
