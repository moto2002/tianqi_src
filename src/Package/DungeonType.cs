using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "DungeonType")]
	[Serializable]
	public class DungeonType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Other", Value = 100)]
			Other = 100,
			[ProtoEnum(Name = "Normal", Value = 101)]
			Normal,
			[ProtoEnum(Name = "Elite", Value = 102)]
			Elite,
			[ProtoEnum(Name = "Team", Value = 103)]
			Team,
			[ProtoEnum(Name = "Society", Value = 112)]
			Society = 112,
			[ProtoEnum(Name = "Arena", Value = 104)]
			Arena = 104,
			[ProtoEnum(Name = "Survival", Value = 106)]
			Survival = 106,
			[ProtoEnum(Name = "Element", Value = 107)]
			Element,
			[ProtoEnum(Name = "Defence", Value = 108)]
			Defence,
			[ProtoEnum(Name = "WildBoss", Value = 116)]
			WildBoss = 116,
			[ProtoEnum(Name = "MultiPvp", Value = 121)]
			MultiPvp = 121,
			[ProtoEnum(Name = "MultiPve", Value = 122)]
			MultiPve
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
