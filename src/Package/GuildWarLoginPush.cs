using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(128), ForSend(128), ProtoContract(Name = "GuildWarLoginPush")]
	[Serializable]
	public class GuildWarLoginPush : IExtensible
	{
		public static readonly short OP = 128;

		private long _hp = -1L;

		private int _reliveCD = -1;

		private int _bornPointId = -1;

		private readonly List<GuildId2Name> _guildId2Name = new List<GuildId2Name>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(-1L)]
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this._hp = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "reliveCD", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int reliveCD
		{
			get
			{
				return this._reliveCD;
			}
			set
			{
				this._reliveCD = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "bornPointId", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int bornPointId
		{
			get
			{
				return this._bornPointId;
			}
			set
			{
				this._bornPointId = value;
			}
		}

		[ProtoMember(4, Name = "guildId2Name", DataFormat = DataFormat.Default)]
		public List<GuildId2Name> guildId2Name
		{
			get
			{
				return this._guildId2Name;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
