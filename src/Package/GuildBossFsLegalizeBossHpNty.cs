using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildBossFsLegalizeBossHpNty")]
	[Serializable]
	public class GuildBossFsLegalizeBossHpNty : IExtensible
	{
		private long _guildId;

		private long _bossHp;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "guildId", DataFormat = DataFormat.TwosComplement)]
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this._guildId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "bossHp", DataFormat = DataFormat.TwosComplement)]
		public long bossHp
		{
			get
			{
				return this._bossHp;
			}
			set
			{
				this._bossHp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
