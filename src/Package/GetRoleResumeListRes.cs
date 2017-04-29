using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(4059), ForSend(4059), ProtoContract(Name = "GetRoleResumeListRes")]
	[Serializable]
	public class GetRoleResumeListRes : IExtensible
	{
		public static readonly short OP = 4059;

		private readonly List<MemberResume> _buddyRoleList = new List<MemberResume>();

		private readonly List<MemberResume> _guildRoleList = new List<MemberResume>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "buddyRoleList", DataFormat = DataFormat.Default)]
		public List<MemberResume> buddyRoleList
		{
			get
			{
				return this._buddyRoleList;
			}
		}

		[ProtoMember(2, Name = "guildRoleList", DataFormat = DataFormat.Default)]
		public List<MemberResume> guildRoleList
		{
			get
			{
				return this._guildRoleList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
