using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YeWaiBOSS_ARRAY")]
	[Serializable]
	public class YeWaiBOSS_ARRAY : IExtensible
	{
		private readonly List<YeWaiBOSS> _items = new List<YeWaiBOSS>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YeWaiBOSS> items
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
