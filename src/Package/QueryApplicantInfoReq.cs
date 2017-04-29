using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(931), ForSend(931), ProtoContract(Name = "QueryApplicantInfoReq")]
	[Serializable]
	public class QueryApplicantInfoReq : IExtensible
	{
		public static readonly short OP = 931;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
