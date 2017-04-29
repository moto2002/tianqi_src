using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(487), ForSend(487), ProtoContract(Name = "LeaveTeamReq")]
	[Serializable]
	public class LeaveTeamReq : IExtensible
	{
		public static readonly short OP = 487;

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
