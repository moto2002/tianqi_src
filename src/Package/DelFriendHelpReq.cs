using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3732), ForSend(3732), ProtoContract(Name = "DelFriendHelpReq")]
	[Serializable]
	public class DelFriendHelpReq : IExtensible
	{
		public static readonly short OP = 3732;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
