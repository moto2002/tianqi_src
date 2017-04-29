using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2213), ForSend(2213), ProtoContract(Name = "DelFriendHelpRes")]
	[Serializable]
	public class DelFriendHelpRes : IExtensible
	{
		public static readonly short OP = 2213;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
