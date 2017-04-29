using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(594), ForSend(594), ProtoContract(Name = "ResumeFieldReq")]
	[Serializable]
	public class ResumeFieldReq : IExtensible
	{
		public static readonly short OP = 594;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
