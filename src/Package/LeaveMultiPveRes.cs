using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(186), ForSend(186), ProtoContract(Name = "LeaveMultiPveRes")]
	[Serializable]
	public class LeaveMultiPveRes : IExtensible
	{
		public static readonly short OP = 186;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
