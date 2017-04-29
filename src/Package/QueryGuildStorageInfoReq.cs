using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3861), ForSend(3861), ProtoContract(Name = "QueryGuildStorageInfoReq")]
	[Serializable]
	public class QueryGuildStorageInfoReq : IExtensible
	{
		public static readonly short OP = 3861;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
