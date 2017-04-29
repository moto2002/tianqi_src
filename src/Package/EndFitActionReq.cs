using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(607), ForSend(607), ProtoContract(Name = "EndFitActionReq")]
	[Serializable]
	public class EndFitActionReq : IExtensible
	{
		public static readonly short OP = 607;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
