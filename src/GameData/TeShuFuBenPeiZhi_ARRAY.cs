using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TeShuFuBenPeiZhi_ARRAY")]
	[Serializable]
	public class TeShuFuBenPeiZhi_ARRAY : IExtensible
	{
		private readonly List<TeShuFuBenPeiZhi> _items = new List<TeShuFuBenPeiZhi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TeShuFuBenPeiZhi> items
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
