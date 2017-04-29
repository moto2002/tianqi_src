using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(678), ForSend(678), ProtoContract(Name = "ExitChallengeRes")]
	[Serializable]
	public class ExitChallengeRes : IExtensible
	{
		public static readonly short OP = 678;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
