using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4074), ForSend(4074), ProtoContract(Name = "KickoffMemberRes")]
	[Serializable]
	public class KickoffMemberRes : IExtensible
	{
		public static readonly short OP = 4074;

		private long _roleId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
