using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4072), ForSend(4072), ProtoContract(Name = "TeamChangeLeaderNty")]
	[Serializable]
	public class TeamChangeLeaderNty : IExtensible
	{
		public static readonly short OP = 4072;

		private long _roleId;

		private int _teamId;

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

		[ProtoMember(2, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
