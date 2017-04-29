using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ChannelType")]
	[Serializable]
	public class ChannelType : IExtensible
	{
		[ProtoContract(Name = "CT")]
		public enum CT
		{
			[ProtoEnum(Name = "Faction", Value = 0)]
			Faction,
			[ProtoEnum(Name = "Private", Value = 1)]
			Private,
			[ProtoEnum(Name = "SingleWorld", Value = 3)]
			SingleWorld = 3,
			[ProtoEnum(Name = "World", Value = 4)]
			World,
			[ProtoEnum(Name = "Team", Value = 5)]
			Team,
			[ProtoEnum(Name = "System", Value = 6)]
			System,
			[ProtoEnum(Name = "TeamOrg", Value = 7)]
			TeamOrg
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
