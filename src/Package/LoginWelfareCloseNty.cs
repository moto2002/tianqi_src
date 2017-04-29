using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2328), ForSend(2328), ProtoContract(Name = "LoginWelfareCloseNty")]
	[Serializable]
	public class LoginWelfareCloseNty : IExtensible
	{
		public static readonly short OP = 2328;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
