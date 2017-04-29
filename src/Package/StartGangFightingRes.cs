using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(861), ForSend(861), ProtoContract(Name = "StartGangFightingRes")]
	[Serializable]
	public class StartGangFightingRes : IExtensible
	{
		public static readonly short OP = 861;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
