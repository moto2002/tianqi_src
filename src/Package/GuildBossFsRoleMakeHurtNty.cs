using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "GuildBossFsRoleMakeHurtNty")]
	[Serializable]
	public class GuildBossFsRoleMakeHurtNty : IExtensible
	{
		[ProtoContract(Name = "GuildBossHurtInfo")]
		[Serializable]
		public class GuildBossHurtInfo : IExtensible
		{
			private long _roleId;

			private int _hurt;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
			public long roleId
			{
				get
				{
					return this._roleId;
				}
				set
				{
					this._roleId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "hurt", DataFormat = DataFormat.TwosComplement)]
			public int hurt
			{
				get
				{
					return this._hurt;
				}
				set
				{
					this._hurt = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private long _guildId;

		private readonly List<GuildBossFsRoleMakeHurtNty.GuildBossHurtInfo> _hurtInfo = new List<GuildBossFsRoleMakeHurtNty.GuildBossHurtInfo>();

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

		[ProtoMember(2, Name = "hurtInfo", DataFormat = DataFormat.Default)]
		public List<GuildBossFsRoleMakeHurtNty.GuildBossHurtInfo> hurtInfo
		{
			get
			{
				return this._hurtInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
