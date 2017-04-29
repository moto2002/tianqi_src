using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(789), ForSend(789), ProtoContract(Name = "ExitElementCopyReq")]
	[Serializable]
	public class ExitElementCopyReq : IExtensible
	{
		public static readonly short OP = 789;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
