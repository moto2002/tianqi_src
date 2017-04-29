using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(897), ForSend(897), ProtoContract(Name = "QueryRankingsInfoRes")]
	[Serializable]
	public class QueryRankingsInfoRes : IExtensible
	{
		public static readonly short OP = 897;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
