using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(89), ForSend(89), ProtoContract(Name = "GetMemberInGuildWarRes")]
	[Serializable]
	public class GetMemberInGuildWarRes : IExtensible
	{
		public static readonly short OP = 89;

		private int _inResourceId = -1;

		private readonly List<MemberInGuildScene> _myMembersInfo = new List<MemberInGuildScene>();

		private readonly List<MemberInGuildScene> _faceMembersInfo = new List<MemberInGuildScene>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "inResourceId", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int inResourceId
		{
			get
			{
				return this._inResourceId;
			}
			set
			{
				this._inResourceId = value;
			}
		}

		[ProtoMember(2, Name = "myMembersInfo", DataFormat = DataFormat.Default)]
		public List<MemberInGuildScene> myMembersInfo
		{
			get
			{
				return this._myMembersInfo;
			}
		}

		[ProtoMember(3, Name = "faceMembersInfo", DataFormat = DataFormat.Default)]
		public List<MemberInGuildScene> faceMembersInfo
		{
			get
			{
				return this._faceMembersInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
