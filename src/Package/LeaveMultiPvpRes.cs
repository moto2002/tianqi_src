using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(162), ForSend(162), ProtoContract(Name = "LeaveMultiPvpRes")]
	[Serializable]
	public class LeaveMultiPvpRes : IExtensible
	{
		public static readonly short OP = 162;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
