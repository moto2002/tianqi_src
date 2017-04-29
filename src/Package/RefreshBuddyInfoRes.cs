using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(816), ForSend(816), ProtoContract(Name = "RefreshBuddyInfoRes")]
	[Serializable]
	public class RefreshBuddyInfoRes : IExtensible
	{
		public static readonly short OP = 816;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
