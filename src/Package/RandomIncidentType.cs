using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "RandomIncidentType")]
	[Serializable]
	public class RandomIncidentType : IExtensible
	{
		[ProtoContract(Name = "IncidentType")]
		public enum IncidentType
		{
			[ProtoEnum(Name = "TOOL", Value = 0)]
			TOOL,
			[ProtoEnum(Name = "MONSTER", Value = 1)]
			MONSTER,
			[ProtoEnum(Name = "MINE", Value = 2)]
			MINE,
			[ProtoEnum(Name = "ROADBLOCK", Value = 3)]
			ROADBLOCK,
			[ProtoEnum(Name = "PETPROPERTY", Value = 4)]
			PETPROPERTY,
			[ProtoEnum(Name = "PLAYERPROPERTY", Value = 5)]
			PLAYERPROPERTY,
			[ProtoEnum(Name = "EMPTY", Value = 6)]
			EMPTY,
			[ProtoEnum(Name = "RECOVRYENERGY", Value = 7)]
			RECOVRYENERGY
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
