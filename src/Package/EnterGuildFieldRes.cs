using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(49), ForSend(49), ProtoContract(Name = "EnterGuildFieldRes")]
	[Serializable]
	public class EnterGuildFieldRes : IExtensible
	{
		public static readonly short OP = 49;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
