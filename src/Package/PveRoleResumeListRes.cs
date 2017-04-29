using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(881), ForSend(881), ProtoContract(Name = "PveRoleResumeListRes")]
	[Serializable]
	public class PveRoleResumeListRes : IExtensible
	{
		public static readonly short OP = 881;

		private readonly List<PveRoleResume> _buddyRoleList = new List<PveRoleResume>();

		private readonly List<PveRoleResume> _guildRoleList = new List<PveRoleResume>();

		private IExtension extensionObject;

		[ProtoMember(2, Name = "buddyRoleList", DataFormat = DataFormat.Default)]
		public List<PveRoleResume> buddyRoleList
		{
			get
			{
				return this._buddyRoleList;
			}
		}

		[ProtoMember(3, Name = "guildRoleList", DataFormat = DataFormat.Default)]
		public List<PveRoleResume> guildRoleList
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
