using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YeWaiBOSSJieJi_ARRAY")]
	[Serializable]
	public class YeWaiBOSSJieJi_ARRAY : IExtensible
	{
		private readonly List<YeWaiBOSSJieJi> _items = new List<YeWaiBOSSJieJi>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YeWaiBOSSJieJi> items
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
