using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(82), ForSend(82), ProtoContract(Name = "RolePreciseMoveToRes")]
	[Serializable]
	public class RolePreciseMoveToRes : IExtensible
	{
		public static readonly short OP = 82;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
