using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(832), ForSend(832), ProtoContract(Name = "StartChallengingReq")]
	[Serializable]
	public class StartChallengingReq : IExtensible
	{
		public static readonly short OP = 832;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
