using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1162), ForSend(1162), ProtoContract(Name = "TestClientReq")]
	[Serializable]
	public class TestClientReq : IExtensible
	{
		public static readonly short OP = 1162;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
