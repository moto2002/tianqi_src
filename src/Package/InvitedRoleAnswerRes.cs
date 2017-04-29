using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(887), ForSend(887), ProtoContract(Name = "InvitedRoleAnswerRes")]
	[Serializable]
	public class InvitedRoleAnswerRes : IExtensible
	{
		public static readonly short OP = 887;

		private ulong _teamId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0f)]
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
