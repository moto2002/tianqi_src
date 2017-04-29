using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3002), ForSend(3002), ProtoContract(Name = "LeavePVPFieldRes")]
	[Serializable]
	public class LeavePVPFieldRes : IExtensible
	{
		public static readonly short OP = 3002;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
