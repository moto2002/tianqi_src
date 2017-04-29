using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5511), ForSend(5511), ProtoContract(Name = "EliteExitRes")]
	[Serializable]
	public class EliteExitRes : IExtensible
	{
		public static readonly short OP = 5511;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
