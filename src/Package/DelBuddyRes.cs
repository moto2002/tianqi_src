using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(445), ForSend(445), ProtoContract(Name = "DelBuddyRes")]
	[Serializable]
	public class DelBuddyRes : IExtensible
	{
		public static readonly short OP = 445;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
