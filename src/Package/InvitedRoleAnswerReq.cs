using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(885), ForSend(885), ProtoContract(Name = "InvitedRoleAnswerReq")]
	[Serializable]
	public class InvitedRoleAnswerReq : IExtensible
	{
		public static readonly short OP = 885;

		private long _inviteRoleId;

		private bool _agree;

		private ulong _teamId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "inviteRoleId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "agree", DataFormat = DataFormat.Default)]
		public bool agree
		{
			get
			{
				return this._agree;
			}
			set
			{
				this._agree = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0f)]
		public ulong teamId
		{
			get
			{
				return this._teamId;
			}
			set
			{
				this._teamId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
