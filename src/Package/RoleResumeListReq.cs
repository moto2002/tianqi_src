using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(9001), ForSend(9001), ProtoContract(Name = "RoleResumeListReq")]
	[Serializable]
	public class RoleResumeListReq : IExtensible
	{
		public static readonly short OP = 9001;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
