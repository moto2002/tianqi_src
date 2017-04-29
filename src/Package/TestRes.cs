using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(311), ForSend(311), ProtoContract(Name = "TestRes")]
	[Serializable]
	public class TestRes : IExtensible
	{
		public static readonly short OP = 311;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
