using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(181), ForSend(181), ProtoContract(Name = "EliteChallengeFirstReq")]
	[Serializable]
	public class EliteChallengeFirstReq : IExtensible
	{
		public static readonly short OP = 181;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
