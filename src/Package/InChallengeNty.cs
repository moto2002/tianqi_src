using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7463), ForSend(7463), ProtoContract(Name = "InChallengeNty")]
	[Serializable]
	public class InChallengeNty : IExtensible
	{
		public static readonly short OP = 7463;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
