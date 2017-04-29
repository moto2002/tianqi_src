using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3664), ForSend(3664), ProtoContract(Name = "DissolveGuildRes")]
	[Serializable]
	public class DissolveGuildRes : IExtensible
	{
		public static readonly short OP = 3664;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
