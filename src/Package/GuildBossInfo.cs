using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GuildBossInfo")]
	[Serializable]
	public class GuildBossInfo : IExtensible
	{
		private int _bossId;

		private long _bossHp;

		private long _bossHpLmt;

		private int _endCD;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "bossId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = true, Name = "bossHpLmt", DataFormat = DataFormat.TwosComplement)]
		public long bossHpLmt
		{
			get
			{
				return this._bossHpLmt;
			}
			set
			{
				this._bossHpLmt = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "endCD", DataFormat = DataFormat.TwosComplement)]
		public int endCD
		{
			get
			{
				return this._endCD;
			}
			set
			{
				this._endCD = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
