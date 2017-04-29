using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(39), ForSend(39), ProtoContract(Name = "ExitExperienceCopyRes")]
	[Serializable]
	public class ExitExperienceCopyRes : IExtensible
	{
		public static readonly short OP = 39;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
