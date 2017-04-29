using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(71), ForSend(71), ProtoContract(Name = "CleanGuildBossCdReq")]
	[Serializable]
	public class CleanGuildBossCdReq : IExtensible
	{
		public static readonly short OP = 71;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
