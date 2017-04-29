using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6732), ForSend(6732), ProtoContract(Name = "EnterGrabRes")]
	[Serializable]
	public class EnterGrabRes : IExtensible
	{
		public static readonly short OP = 6732;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
