using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5381), ForSend(5381), ProtoContract(Name = "ProtectFightRes")]
	[Serializable]
	public class ProtectFightRes : IExtensible
	{
		public static readonly short OP = 5381;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
