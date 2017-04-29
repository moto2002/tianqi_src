using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4070), ForSend(4070), ProtoContract(Name = "PartnerLeaveTeamReq")]
	[Serializable]
	public class PartnerLeaveTeamReq : IExtensible
	{
		public static readonly short OP = 4070;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
