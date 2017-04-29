using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3456), ForSend(3456), ProtoContract(Name = "ChangeModeRes")]
	[Serializable]
	public class ChangeModeRes : IExtensible
	{
		public static readonly short OP = 3456;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
