using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(834), ForSend(834), ProtoContract(Name = "StartChallengingRes")]
	[Serializable]
	public class StartChallengingRes : IExtensible
	{
		public static readonly short OP = 834;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
