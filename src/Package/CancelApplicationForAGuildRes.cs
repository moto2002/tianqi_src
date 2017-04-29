using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1200), ForSend(1200), ProtoContract(Name = "CancelApplicationForAGuildRes")]
	[Serializable]
	public class CancelApplicationForAGuildRes : IExtensible
	{
		public static readonly short OP = 1200;

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
