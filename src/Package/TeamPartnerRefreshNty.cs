using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4065), ForSend(4065), ProtoContract(Name = "TeamPartnerRefreshNty")]
	[Serializable]
	public class TeamPartnerRefreshNty : IExtensible
	{
		public static readonly short OP = 4065;

		private TeamRefreshType.ENUM _refreshType;

		private MemberResume _memberResume;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "refreshType", DataFormat = DataFormat.TwosComplement)]
		public TeamRefreshType.ENUM refreshType
		{
			get
			{
				return this._refreshType;
			}
			set
			{
				this._refreshType = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "memberResume", DataFormat = DataFormat.Default)]
		public MemberResume memberResume
		{
			get
			{
				return this._memberResume;
			}
			set
			{
				this._memberResume = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
