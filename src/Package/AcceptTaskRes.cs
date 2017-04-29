using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7822), ForSend(7822), ProtoContract(Name = "AcceptTaskRes")]
	[Serializable]
	public class AcceptTaskRes : IExtensible
	{
		public static readonly short OP = 7822;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
