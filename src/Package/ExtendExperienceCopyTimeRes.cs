using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(44), ForSend(44), ProtoContract(Name = "ExtendExperienceCopyTimeRes")]
	[Serializable]
	public class ExtendExperienceCopyTimeRes : IExtensible
	{
		public static readonly short OP = 44;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
