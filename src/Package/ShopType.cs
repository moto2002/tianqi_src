using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ShopType")]
	[Serializable]
	public class ShopType : IExtensible
	{
		[ProtoContract(Name = "ST")]
		public enum ST
		{
			[ProtoEnum(Name = "NpcShop", Value = 1)]
			NpcShop = 1
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
