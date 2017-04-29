using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(126), ForSend(126), ProtoContract(Name = "GuildWarResBriefNty")]
	[Serializable]
	public class GuildWarResBriefNty : IExtensible
	{
		[ProtoContract(Name = "GuildWarResBrief")]
		[Serializable]
		public class GuildWarResBrief : IExtensible
		{
			private int _resourceId;

			private int _myMemberCount;

			private int _faceMemberCount;

			private long _ownerGuildId;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "resourceId", DataFormat = DataFormat.TwosComplement)]
			public int resourceId
			{
				get
				{
					return this._resourceId;
				}
				set
				{
					this._resourceId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "myMemberCount", DataFormat = DataFormat.TwosComplement)]
			public int myMemberCount
			{
				get
				{
					return this._myMemberCount;
				}
				set
				{
					this._myMemberCount = value;
				}
			}

			[ProtoMember(3, IsRequired = true, Name = "faceMemberCount", DataFormat = DataFormat.TwosComplement)]
			public int faceMemberCount
			{
				get
				{
					return this._faceMemberCount;
				}
				set
				{
					this._faceMemberCount = value;
				}
			}

			[ProtoMember(4, IsRequired = false, Name = "ownerGuildId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
			public long ownerGuildId
			{
				get
				{
					return this._ownerGuildId;
				}
				set
				{
					this._ownerGuildId = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 126;

		private readonly List<GuildWarResBriefNty.GuildWarResBrief> _guildWarResBrief = new List<GuildWarResBriefNty.GuildWarResBrief>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "guildWarResBrief", DataFormat = DataFormat.Default)]
		public List<GuildWarResBriefNty.GuildWarResBrief> guildWarResBrief
		{
			get
			{
				return this._guildWarResBrief;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
