using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(843), ForSend(843), ProtoContract(Name = "EnterWaitingRoomRes")]
	[Serializable]
	public class EnterWaitingRoomRes : IExtensible
	{
		public static readonly short OP = 843;

		private int _bornPointId = -1;

		private readonly List<GuildId2Name> _guildId2Name = new List<GuildId2Name>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "bornPointId", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
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

		[ProtoMember(2, Name = "guildId2Name", DataFormat = DataFormat.Default)]
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
