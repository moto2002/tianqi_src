using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(48), ForSend(48), ProtoContract(Name = "EnterGuildFieldReq")]
	[Serializable]
	public class EnterGuildFieldReq : IExtensible
	{
		public static readonly short OP = 48;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
