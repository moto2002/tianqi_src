using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3708), ForSend(3708), ProtoContract(Name = "RuneCompositeRes")]
	[Serializable]
	public class RuneCompositeRes : IExtensible
	{
		public static readonly short OP = 3708;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
