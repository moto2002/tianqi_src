using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(605), ForSend(605), ProtoContract(Name = "LeavePVPFieldReq")]
	[Serializable]
	public class LeavePVPFieldReq : IExtensible
	{
		public static readonly short OP = 605;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
