using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(746), ForSend(746), ProtoContract(Name = "QueryGuildInfoRes")]
	[Serializable]
	public class QueryGuildInfoRes : IExtensible
	{
		public static readonly short OP = 746;

		private readonly List<GuildBriefInfo> _guildInfos = new List<GuildBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "guildInfos", DataFormat = DataFormat.Default)]
		public List<GuildBriefInfo> guildInfos
		{
			get
			{
				return this._guildInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
