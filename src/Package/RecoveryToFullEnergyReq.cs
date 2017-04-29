using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1027), ForSend(1027), ProtoContract(Name = "RecoveryToFullEnergyReq")]
	[Serializable]
	public class RecoveryToFullEnergyReq : IExtensible
	{
		public static readonly short OP = 1027;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
