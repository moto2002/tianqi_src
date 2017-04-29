using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5754), ForSend(5754), ProtoContract(Name = "OpenTitleReq")]
	[Serializable]
	public class OpenTitleReq : IExtensible
	{
		public static readonly short OP = 5754;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
