using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(663), ForSend(663), ProtoContract(Name = "SaveGuideInfoRes")]
	[Serializable]
	public class SaveGuideInfoRes : IExtensible
	{
		public static readonly short OP = 663;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
