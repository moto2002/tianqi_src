using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5733), ForSend(5733), ProtoContract(Name = "ZeroCityRefreshRes")]
	[Serializable]
	public class ZeroCityRefreshRes : IExtensible
	{
		public static readonly short OP = 5733;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
