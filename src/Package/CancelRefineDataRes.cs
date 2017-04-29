using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1997), ForSend(1997), ProtoContract(Name = "CancelRefineDataRes")]
	[Serializable]
	public class CancelRefineDataRes : IExtensible
	{
		public static readonly short OP = 1997;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
