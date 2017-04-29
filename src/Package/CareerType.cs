using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "CareerType")]
	[Serializable]
	public class CareerType : IExtensible
	{
		[ProtoContract(Name = "CT")]
		public enum CT
		{
			[ProtoEnum(Name = "Swordsman", Value = 1)]
			Swordsman = 1,
			[ProtoEnum(Name = "Gunman", Value = 2)]
			Gunman,
			[ProtoEnum(Name = "SwordSister", Value = 3)]
			SwordSister,
			[ProtoEnum(Name = "MadWarrior", Value = 4)]
			MadWarrior,
			[ProtoEnum(Name = "ShieldWarrior", Value = 5)]
			ShieldWarrior,
			[ProtoEnum(Name = "Hammer", Value = 6)]
			Hammer,
			[ProtoEnum(Name = "Engineer", Value = 7)]
			Engineer,
			[ProtoEnum(Name = "Thug", Value = 8)]
			Thug,
			[ProtoEnum(Name = "KnifeHoly", Value = 9)]
			KnifeHoly
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
