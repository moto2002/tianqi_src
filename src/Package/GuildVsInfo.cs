using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "GuildVsInfo")]
	[Serializable]
	public class GuildVsInfo : IExtensible
	{
		private readonly List<GuildParticipantInfo> _guildsInfo = new List<GuildParticipantInfo>();

		private long _winnerId;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "guildsInfo", DataFormat = DataFormat.Default)]
		public List<GuildParticipantInfo> guildsInfo
		{
			get
			{
				return this._guildsInfo;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "winnerId", DataFormat = DataFormat.TwosComplement)]
		public long winnerId
		{
			get
			{
				return this._winnerId;
			}
			set
			{
				this._winnerId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
