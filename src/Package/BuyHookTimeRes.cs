using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3412), ForSend(3412), ProtoContract(Name = "BuyHookTimeRes")]
	[Serializable]
	public class BuyHookTimeRes : IExtensible
	{
		public static readonly short OP = 3412;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
