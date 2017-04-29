using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4629), ForSend(4629), ProtoContract(Name = "FriendProtectAnswerRes")]
	[Serializable]
	public class FriendProtectAnswerRes : IExtensible
	{
		public static readonly short OP = 4629;

		private long _inviteRoleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "inviteRoleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long inviteRoleId
		{
			get
			{
				return this._inviteRoleId;
			}
			set
			{
				this._inviteRoleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
