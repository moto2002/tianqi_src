using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "AttrType")]
	[Serializable]
	public class AttrType : IExtensible
	{
		[ProtoContract(Name = "AT")]
		public enum AT
		{
			[ProtoEnum(Name = "HP", Value = 0)]
			HP,
			[ProtoEnum(Name = "ATTACK", Value = 1)]
			ATTACK,
			[ProtoEnum(Name = "DEFENCE", Value = 2)]
			DEFENCE,
			[ProtoEnum(Name = "CRIT", Value = 3)]
			CRIT,
			[ProtoEnum(Name = "HIT", Value = 4)]
			HIT,
			[ProtoEnum(Name = "DEX", Value = 5)]
			DEX,
			[ProtoEnum(Name = "PENETRATION", Value = 6)]
			PENETRATION,
			[ProtoEnum(Name = "VIGOUR", Value = 7)]
			VIGOUR,
			[ProtoEnum(Name = "PARRY", Value = 8)]
			PARRY,
			[ProtoEnum(Name = "AFFINITY", Value = 9)]
			AFFINITY,
			[ProtoEnum(Name = "FIGHTING", Value = 10)]
			FIGHTING,
			[ProtoEnum(Name = "ACTION_POINT", Value = 11)]
			ACTION_POINT
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
