using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildBuildType")]
	[Serializable]
	public class GuildBuildType : IExtensible
	{
		[ProtoContract(Name = "GBT")]
		public enum GBT
		{
			[ProtoEnum(Name = "GUILD_DONATE", Value = 1)]
			GUILD_DONATE = 1,
			[ProtoEnum(Name = "GUILD_TASK", Value = 2)]
			GUILD_TASK
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
