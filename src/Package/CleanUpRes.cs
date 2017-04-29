using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(403), ForSend(403), ProtoContract(Name = "CleanUpRes")]
	[Serializable]
	public class CleanUpRes : IExtensible
	{
		public static readonly short OP = 403;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
