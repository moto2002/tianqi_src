using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(8721), ForSend(8721), ProtoContract(Name = "FirstOpenRes")]
	[Serializable]
	public class FirstOpenRes : IExtensible
	{
		public static readonly short OP = 8721;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
