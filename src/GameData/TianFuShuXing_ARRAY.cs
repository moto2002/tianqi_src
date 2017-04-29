using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TianFuShuXing_ARRAY")]
	[Serializable]
	public class TianFuShuXing_ARRAY : IExtensible
	{
		private readonly List<TianFuShuXing> _items = new List<TianFuShuXing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<TianFuShuXing> items
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
