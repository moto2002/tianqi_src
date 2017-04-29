using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BagType")]
	[Serializable]
	public class BagType : IExtensible
	{
		[ProtoContract(Name = "BT")]
		public enum BT
		{
			[ProtoEnum(Name = "Bag", Value = 0)]
			Bag,
			[ProtoEnum(Name = "Role_bag", Value = 1)]
			Role_bag,
			[ProtoEnum(Name = "Fashion_bag", Value = 2)]
			Fashion_bag,
			[ProtoEnum(Name = "Max", Value = 3)]
			Max
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
