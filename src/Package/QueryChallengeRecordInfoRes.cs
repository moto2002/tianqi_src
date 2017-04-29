using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1166), ForSend(1166), ProtoContract(Name = "QueryChallengeRecordInfoRes")]
	[Serializable]
	public class QueryChallengeRecordInfoRes : IExtensible
	{
		public static readonly short OP = 1166;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
