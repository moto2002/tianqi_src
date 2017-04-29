using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(689), ForSend(689), ProtoContract(Name = "InviteAgreeInfos")]
	[Serializable]
	public class InviteAgreeInfos : IExtensible
	{
		public static readonly short OP = 689;

		private readonly List<BuddyInfo> _otherInviteInfos = new List<BuddyInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "otherInviteInfos", DataFormat = DataFormat.Default)]
		public List<BuddyInfo> otherInviteInfos
		{
			get
			{
				return this._otherInviteInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
