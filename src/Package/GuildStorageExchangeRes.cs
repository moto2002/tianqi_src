using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3864), ForSend(3864), ProtoContract(Name = "GuildStorageExchangeRes")]
	[Serializable]
	public class GuildStorageExchangeRes : IExtensible
	{
		public static readonly short OP = 3864;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
