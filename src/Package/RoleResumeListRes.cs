using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(9002), ForSend(9002), ProtoContract(Name = "RoleResumeListRes")]
	[Serializable]
	public class RoleResumeListRes : IExtensible
	{
		public static readonly short OP = 9002;

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
