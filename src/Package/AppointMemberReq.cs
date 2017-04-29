using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3661), ForSend(3661), ProtoContract(Name = "AppointMemberReq")]
	[Serializable]
	public class AppointMemberReq : IExtensible
	{
		public static readonly short OP = 3661;

		private MemberTitleType.MTT _title;

		private long _roleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "title", DataFormat = DataFormat.TwosComplement)]
		public MemberTitleType.MTT title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
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
