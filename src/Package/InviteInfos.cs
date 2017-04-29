using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(495), ForSend(495), ProtoContract(Name = "InviteInfos")]
	[Serializable]
	public class InviteInfos : IExtensible
	{
		public static readonly short OP = 495;

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
