using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4069), ForSend(4069), ProtoContract(Name = "AppointJoinTeamInfoRes")]
	[Serializable]
	public class AppointJoinTeamInfoRes : IExtensible
	{
		public static readonly short OP = 4069;

		private int _teamId;

		private int _cdTime;

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

		[ProtoMember(2, IsRequired = false, Name = "cdTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cdTime
		{
			get
			{
				return this._cdTime;
			}
			set
			{
				this._cdTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
