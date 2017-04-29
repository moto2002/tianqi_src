using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(609), ForSend(609), ProtoContract(Name = "EndFitActionRes")]
	[Serializable]
	public class EndFitActionRes : IExtensible
	{
		public static readonly short OP = 609;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
