using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1198), ForSend(1198), ProtoContract(Name = "CancelApplicationForAGuildReq")]
	[Serializable]
	public class CancelApplicationForAGuildReq : IExtensible
	{
		public static readonly short OP = 1198;

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
