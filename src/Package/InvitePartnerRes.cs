using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4061), ForSend(4061), ProtoContract(Name = "InvitePartnerRes")]
	[Serializable]
	public class InvitePartnerRes : IExtensible
	{
		public static readonly short OP = 4061;

		private long _roleId;

		private int _cdTime;

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
