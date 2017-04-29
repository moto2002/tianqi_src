using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(804), ForSend(804), ProtoContract(Name = "DrawAllMailAttachReq")]
	[Serializable]
	public class DrawAllMailAttachReq : IExtensible
	{
		public static readonly short OP = 804;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
