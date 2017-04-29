using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6782), ForSend(6782), ProtoContract(Name = "LookTitleReq")]
	[Serializable]
	public class LookTitleReq : IExtensible
	{
		public static readonly short OP = 6782;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
