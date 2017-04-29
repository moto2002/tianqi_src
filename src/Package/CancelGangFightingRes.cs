using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(858), ForSend(858), ProtoContract(Name = "CancelGangFightingRes")]
	[Serializable]
	public class CancelGangFightingRes : IExtensible
	{
		public static readonly short OP = 858;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
