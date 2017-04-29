using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1164), ForSend(1164), ProtoContract(Name = "QueryChallengeRecordInfoReq")]
	[Serializable]
	public class QueryChallengeRecordInfoReq : IExtensible
	{
		public static readonly short OP = 1164;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
