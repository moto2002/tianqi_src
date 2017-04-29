using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(297), ForSend(297), ProtoContract(Name = "PingRes")]
	[Serializable]
	public class PingRes : IExtensible
	{
		public static readonly short OP = 297;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
