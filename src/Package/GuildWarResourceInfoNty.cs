using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(122), ForSend(122), ProtoContract(Name = "GuildWarResourceInfoNty")]
	[Serializable]
	public class GuildWarResourceInfoNty : IExtensible
	{
		[ProtoContract(Name = "GuildResourceInfo")]
		[Serializable]
		public class GuildResourceInfo : IExtensible
		{
			private long _guildId;

			private int _resource;

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

			[ProtoMember(2, IsRequired = true, Name = "resource", DataFormat = DataFormat.TwosComplement)]
			public int resource
			{
				get
				{
					return this._resource;
				}
				set
				{
					this._resource = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 122;

		private readonly List<GuildWarResourceInfoNty.GuildResourceInfo> _guildResourceInfo = new List<GuildWarResourceInfoNty.GuildResourceInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "guildResourceInfo", DataFormat = DataFormat.Default)]
		public List<GuildWarResourceInfoNty.GuildResourceInfo> guildResourceInfo
		{
			get
			{
				return this._guildResourceInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
