using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(109), ForSend(109), ProtoContract(Name = "EliteCancelMatchReq")]
	[Serializable]
	public class EliteCancelMatchReq : IExtensible
	{
		public static readonly short OP = 109;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
