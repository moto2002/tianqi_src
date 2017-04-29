using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(30006), ForSend(30006), ProtoContract(Name = "BuyEnergyRes")]
	[Serializable]
	public class BuyEnergyRes : IExtensible
	{
		public static readonly short OP = 30006;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
