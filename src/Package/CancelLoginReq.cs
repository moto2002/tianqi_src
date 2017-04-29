using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(177), ForSend(177), ProtoContract(Name = "CancelLoginReq")]
	[Serializable]
	public class CancelLoginReq : IExtensible
	{
		public static readonly short OP = 177;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
