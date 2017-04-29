using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(90), ForSend(90), ProtoContract(Name = "EligibilityGuildInfoReq")]
	[Serializable]
	public class EligibilityGuildInfoReq : IExtensible
	{
		public static readonly short OP = 90;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
