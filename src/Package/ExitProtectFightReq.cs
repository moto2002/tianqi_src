using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6563), ForSend(6563), ProtoContract(Name = "ExitProtectFightReq")]
	[Serializable]
	public class ExitProtectFightReq : IExtensible
	{
		public static readonly short OP = 6563;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
