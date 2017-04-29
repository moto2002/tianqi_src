using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "WeaponPosType")]
	[Serializable]
	public class WeaponPosType : IExtensible
	{
		[ProtoContract(Name = "WPT")]
		public enum WPT
		{
			[ProtoEnum(Name = "Weapon", Value = 1)]
			Weapon = 1,
			[ProtoEnum(Name = "Shirt", Value = 2)]
			Shirt,
			[ProtoEnum(Name = "Pant", Value = 3)]
			Pant,
			[ProtoEnum(Name = "Shoe", Value = 4)]
			Shoe,
			[ProtoEnum(Name = "Waist", Value = 5)]
			Waist,
			[ProtoEnum(Name = "Necklace", Value = 6)]
			Necklace,
			[ProtoEnum(Name = "Part7", Value = 7)]
			Part7,
			[ProtoEnum(Name = "Part8", Value = 8)]
			Part8,
			[ProtoEnum(Name = "Part9", Value = 9)]
			Part9,
			[ProtoEnum(Name = "Part10", Value = 10)]
			Part10,
			[ProtoEnum(Name = "Experience", Value = 99)]
			Experience = 99
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
