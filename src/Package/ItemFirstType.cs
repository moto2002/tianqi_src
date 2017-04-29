using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ItemFirstType")]
	[Serializable]
	public class ItemFirstType : IExtensible
	{
		[ProtoContract(Name = "IFT")]
		public enum IFT
		{
			[ProtoEnum(Name = "Equipment", Value = 1)]
			Equipment = 1,
			[ProtoEnum(Name = "Fragment", Value = 2)]
			Fragment,
			[ProtoEnum(Name = "Consume", Value = 3)]
			Consume,
			[ProtoEnum(Name = "Money", Value = 4)]
			Money,
			[ProtoEnum(Name = "Gem", Value = 5)]
			Gem,
			[ProtoEnum(Name = "Pet", Value = 6)]
			Pet
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
