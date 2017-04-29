using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(630), ForSend(630), ProtoContract(Name = "ReceiveAwardRes")]
	[Serializable]
	public class ReceiveAwardRes : IExtensible
	{
		public static readonly short OP = 630;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
