using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4058), ForSend(4058), ProtoContract(Name = "GetRoleResumeListReq")]
	[Serializable]
	public class GetRoleResumeListReq : IExtensible
	{
		public static readonly short OP = 4058;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
