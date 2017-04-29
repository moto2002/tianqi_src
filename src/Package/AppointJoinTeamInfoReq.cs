using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4068), ForSend(4068), ProtoContract(Name = "AppointJoinTeamInfoReq")]
	[Serializable]
	public class AppointJoinTeamInfoReq : IExtensible
	{
		public static readonly short OP = 4068;

		private int _teamId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "teamId", DataFormat = DataFormat.TwosComplement)]
		public int teamId
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
