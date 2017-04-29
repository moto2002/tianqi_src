using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(170), ForSend(170), ProtoContract(Name = "LeaveMultiPvpReq")]
	[Serializable]
	public class LeaveMultiPvpReq : IExtensible
	{
		public static readonly short OP = 170;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
