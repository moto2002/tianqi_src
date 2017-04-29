using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(538), ForSend(538), ProtoContract(Name = "PauseFieldReq")]
	[Serializable]
	public class PauseFieldReq : IExtensible
	{
		public static readonly short OP = 538;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
