using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(854), ForSend(854), ProtoContract(Name = "KickOffGuildMemberRes")]
	[Serializable]
	public class KickOffGuildMemberRes : IExtensible
	{
		public static readonly short OP = 854;

		private long _roleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "roleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
