using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3211), ForSend(3211), ProtoContract(Name = "DefendFightRes")]
	[Serializable]
	public class DefendFightRes : IExtensible
	{
		public static readonly short OP = 3211;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
