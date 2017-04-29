using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(751), ForSend(751), ProtoContract(Name = "SearchGuildInfoRes")]
	[Serializable]
	public class SearchGuildInfoRes : IExtensible
	{
		public static readonly short OP = 751;

		private readonly List<GuildBriefInfo> _guildInfos = new List<GuildBriefInfo>();

		private int _nPage = 1;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "guildInfos", DataFormat = DataFormat.Default)]
		public List<GuildBriefInfo> guildInfos
		{
			get
			{
				return this._guildInfos;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "nPage", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int nPage
		{
			get
			{
				return this._nPage;
			}
			set
			{
				this._nPage = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
