using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(776), ForSend(776), ProtoContract(Name = "ChangeTeamLeaderNty")]
	[Serializable]
	public class ChangeTeamLeaderNty : IExtensible
	{
		public static readonly short OP = 776;

		private long _roleId;

		private ulong _teamId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
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
