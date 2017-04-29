using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "LuckDrawFormationType")]
	[Serializable]
	public class LuckDrawFormationType : IExtensible
	{
		[ProtoContract(Name = "FORMATION_TYPE")]
		public enum FORMATION_TYPE
		{
			[ProtoEnum(Name = "Coupon", Value = 1)]
			Coupon = 1,
			[ProtoEnum(Name = "Diamond", Value = 2)]
			Diamond,
			[ProtoEnum(Name = "Diamonds", Value = 3)]
			Diamonds
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
