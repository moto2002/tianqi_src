using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1801), ForSend(1801), ProtoContract(Name = "SaveNotCompleteRes")]
	[Serializable]
	public class SaveNotCompleteRes : IExtensible
	{
		public static readonly short OP = 1801;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
