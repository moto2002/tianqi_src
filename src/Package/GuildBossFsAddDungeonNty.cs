using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildBossFsAddDungeonNty")]
	[Serializable]
	public class GuildBossFsAddDungeonNty : IExtensible
	{
		private long _guildId;

		private string _fieldId;

		private long _roleId;

		private int _bossId;

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

		[ProtoMember(2, IsRequired = true, Name = "fieldId", DataFormat = DataFormat.Default)]
		public string fieldId
		{
			get
			{
				return this._fieldId;
			}
			set
			{
				this._fieldId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "bossId", DataFormat = DataFormat.TwosComplement)]
		public int bossId
		{
			get
			{
				return this._bossId;
			}
			set
			{
				this._bossId = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "bossHp", DataFormat = DataFormat.TwosComplement)]
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
