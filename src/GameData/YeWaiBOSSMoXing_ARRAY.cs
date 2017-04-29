using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "YeWaiBOSSMoXing_ARRAY")]
	[Serializable]
	public class YeWaiBOSSMoXing_ARRAY : IExtensible
	{
		private readonly List<YeWaiBOSSMoXing> _items = new List<YeWaiBOSSMoXing>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<YeWaiBOSSMoXing> items
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
