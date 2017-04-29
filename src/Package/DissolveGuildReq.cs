using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3643), ForSend(3643), ProtoContract(Name = "DissolveGuildReq")]
	[Serializable]
	public class DissolveGuildReq : IExtensible
	{
		public static readonly short OP = 3643;

		private long _guildId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "guildId", DataFormat = DataFormat.TwosComplement)]
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
