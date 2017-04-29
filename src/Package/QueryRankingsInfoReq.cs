using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(895), ForSend(895), ProtoContract(Name = "QueryRankingsInfoReq")]
	[Serializable]
	public class QueryRankingsInfoReq : IExtensible
	{
		public static readonly short OP = 895;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
