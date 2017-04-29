using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(581), ForSend(581), ProtoContract(Name = "ChallengeMirrorRes")]
	[Serializable]
	public class ChallengeMirrorRes : IExtensible
	{
		public static readonly short OP = 581;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
