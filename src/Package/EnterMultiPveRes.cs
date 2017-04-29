using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(193), ForSend(193), ProtoContract(Name = "EnterMultiPveRes")]
	[Serializable]
	public class EnterMultiPveRes : IExtensible
	{
		public static readonly short OP = 193;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
