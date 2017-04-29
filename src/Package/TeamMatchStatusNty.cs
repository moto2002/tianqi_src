using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(216), ForSend(216), ProtoContract(Name = "TeamMatchStatusNty")]
	[Serializable]
	public class TeamMatchStatusNty : IExtensible
	{
		public static readonly short OP = 216;

		private TeamMatchStatus.ENUM _matchStatus;

		private ulong _teamId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "matchStatus", DataFormat = DataFormat.TwosComplement)]
		public TeamMatchStatus.ENUM matchStatus
		{
			get
			{
				return this._matchStatus;
			}
			set
			{
				this._matchStatus = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0f)]
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
