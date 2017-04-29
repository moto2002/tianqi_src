using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(43), ForSend(43), ProtoContract(Name = "ExtendExperienceCopyTimeReq")]
	[Serializable]
	public class ExtendExperienceCopyTimeReq : IExtensible
	{
		public static readonly short OP = 43;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
