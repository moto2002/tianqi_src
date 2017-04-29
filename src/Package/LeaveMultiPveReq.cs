using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(645), ForSend(645), ProtoContract(Name = "LeaveMultiPveReq")]
	[Serializable]
	public class LeaveMultiPveReq : IExtensible
	{
		public static readonly short OP = 645;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
