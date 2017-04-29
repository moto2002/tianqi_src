using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3101), ForSend(3101), ProtoContract(Name = "ReChallengeRes")]
	[Serializable]
	public class ReChallengeRes : IExtensible
	{
		public static readonly short OP = 3101;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
