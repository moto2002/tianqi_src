using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5522), ForSend(5522), ProtoContract(Name = "GetUpdateAwardRes")]
	[Serializable]
	public class GetUpdateAwardRes : IExtensible
	{
		public static readonly short OP = 5522;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
