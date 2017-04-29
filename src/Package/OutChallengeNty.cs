using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7465), ForSend(7465), ProtoContract(Name = "OutChallengeNty")]
	[Serializable]
	public class OutChallengeNty : IExtensible
	{
		public static readonly short OP = 7465;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
