using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(139), ForSend(139), ProtoContract(Name = "GetRecommendFriendListRes")]
	[Serializable]
	public class GetRecommendFriendListRes : IExtensible
	{
		public static readonly short OP = 139;

		private readonly List<BuddyInfo> _Info = new List<BuddyInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "Info", DataFormat = DataFormat.Default)]
		public List<BuddyInfo> Info
		{
			get
			{
				return this._Info;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
