using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(999), ForSend(999), ProtoContract(Name = "QueryFightRecordInfoReq")]
	[Serializable]
	public class QueryFightRecordInfoReq : IExtensible
	{
		public static readonly short OP = 999;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
