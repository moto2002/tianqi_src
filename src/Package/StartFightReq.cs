using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(215), ForSend(215), ProtoContract(Name = "StartFightReq")]
	[Serializable]
	public class StartFightReq : IExtensible
	{
		public static readonly short OP = 215;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
