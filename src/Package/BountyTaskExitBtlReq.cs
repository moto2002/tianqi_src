using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6832), ForSend(6832), ProtoContract(Name = "BountyTaskExitBtlReq")]
	[Serializable]
	public class BountyTaskExitBtlReq : IExtensible
	{
		public static readonly short OP = 6832;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
