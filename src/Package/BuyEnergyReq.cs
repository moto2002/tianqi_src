using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(30005), ForSend(30005), ProtoContract(Name = "BuyEnergyReq")]
	[Serializable]
	public class BuyEnergyReq : IExtensible
	{
		public static readonly short OP = 30005;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
