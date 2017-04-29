using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1178), ForSend(1178), ProtoContract(Name = "MakeAnApplicationForAGuildReq")]
	[Serializable]
	public class MakeAnApplicationForAGuildReq : IExtensible
	{
		public static readonly short OP = 1178;

		private long _guildId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
