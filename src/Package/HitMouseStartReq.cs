using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6583), ForSend(6583), ProtoContract(Name = "HitMouseStartReq")]
	[Serializable]
	public class HitMouseStartReq : IExtensible
	{
		public static readonly short OP = 6583;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
