using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4083), ForSend(4083), ProtoContract(Name = "AppointJoinTeamInfoNty")]
	[Serializable]
	public class AppointJoinTeamInfoNty : IExtensible
	{
		public static readonly short OP = 4083;

		private MemberResume _resume;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "resume", DataFormat = DataFormat.Default)]
		public MemberResume resume
		{
			get
			{
				return this._resume;
			}
			set
			{
				this._resume = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
