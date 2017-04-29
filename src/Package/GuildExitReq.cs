using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3670), ForSend(3670), ProtoContract(Name = "GuildExitReq")]
	[Serializable]
	public class GuildExitReq : IExtensible
	{
		public static readonly short OP = 3670;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
