using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildWarTimeStep")]
	[Serializable]
	public class GuildWarTimeStep : IExtensible
	{
		[ProtoContract(Name = "GWTS")]
		public enum GWTS
		{
			[ProtoEnum(Name = "NORMAL", Value = 0)]
			NORMAL,
			[ProtoEnum(Name = "ELIGIBILITY", Value = 1)]
			ELIGIBILITY,
			[ProtoEnum(Name = "HALF_MATCH1_BEF", Value = 2)]
			HALF_MATCH1_BEF,
			[ProtoEnum(Name = "HALF_MATCH1_BEG", Value = 3)]
			HALF_MATCH1_BEG,
			[ProtoEnum(Name = "HALF_MATCH1_END", Value = 4)]
			HALF_MATCH1_END,
			[ProtoEnum(Name = "HALF_MATCH2_BEF", Value = 5)]
			HALF_MATCH2_BEF,
			[ProtoEnum(Name = "HALF_MATCH2_BEG", Value = 6)]
			HALF_MATCH2_BEG,
			[ProtoEnum(Name = "HALF_MATCH2_END", Value = 7)]
			HALF_MATCH2_END,
			[ProtoEnum(Name = "FINAL_MATCH_BEF", Value = 8)]
			FINAL_MATCH_BEF,
			[ProtoEnum(Name = "FINAL_MATCH_BEG", Value = 9)]
			FINAL_MATCH_BEG,
			[ProtoEnum(Name = "FINAL_MATCH_END", Value = 10)]
			FINAL_MATCH_END
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
