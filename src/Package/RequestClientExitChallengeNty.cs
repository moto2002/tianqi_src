using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1290), ForSend(1290), ProtoContract(Name = "RequestClientExitChallengeNty")]
	[Serializable]
	public class RequestClientExitChallengeNty : IExtensible
	{
		public static readonly short OP = 1290;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
