using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(27271), ForSend(27271), ProtoContract(Name = "ReqOpenRefineBody")]
	[Serializable]
	public class ReqOpenRefineBody : IExtensible
	{
		public static readonly short OP = 27271;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
