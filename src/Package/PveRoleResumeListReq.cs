using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(879), ForSend(879), ProtoContract(Name = "PveRoleResumeListReq")]
	[Serializable]
	public class PveRoleResumeListReq : IExtensible
	{
		public static readonly short OP = 879;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
