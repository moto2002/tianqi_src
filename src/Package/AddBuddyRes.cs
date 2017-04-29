using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(433), ForSend(433), ProtoContract(Name = "AddBuddyRes")]
	[Serializable]
	public class AddBuddyRes : IExtensible
	{
		public static readonly short OP = 433;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
