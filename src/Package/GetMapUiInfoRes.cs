using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1927), ForSend(1927), ProtoContract(Name = "GetMapUiInfoRes")]
	[Serializable]
	public class GetMapUiInfoRes : IExtensible
	{
		public static readonly short OP = 1927;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
