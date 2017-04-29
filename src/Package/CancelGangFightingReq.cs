using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(856), ForSend(856), ProtoContract(Name = "CancelGangFightingReq")]
	[Serializable]
	public class CancelGangFightingReq : IExtensible
	{
		public static readonly short OP = 856;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
