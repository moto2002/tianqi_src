using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(622), ForSend(622), ProtoContract(Name = "EnterPVPFieldReq")]
	[Serializable]
	public class EnterPVPFieldReq : IExtensible
	{
		public static readonly short OP = 622;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
