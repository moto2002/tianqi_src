using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1001), ForSend(1001), ProtoContract(Name = "QueryFightRecordInfoRes")]
	[Serializable]
	public class QueryFightRecordInfoRes : IExtensible
	{
		public static readonly short OP = 1001;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
