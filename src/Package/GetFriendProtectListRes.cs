using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2932), ForSend(2932), ProtoContract(Name = "GetFriendProtectListRes")]
	[Serializable]
	public class GetFriendProtectListRes : IExtensible
	{
		public static readonly short OP = 2932;

		private readonly List<FriendProtectFightInfo> _infoList = new List<FriendProtectFightInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infoList", DataFormat = DataFormat.Default)]
		public List<FriendProtectFightInfo> infoList
		{
			get
			{
				return this._infoList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
