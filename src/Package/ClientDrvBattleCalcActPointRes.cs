using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1247), ForSend(1247), ProtoContract(Name = "ClientDrvBattleCalcActPointRes")]
	[Serializable]
	public class ClientDrvBattleCalcActPointRes : IExtensible
	{
		public static readonly short OP = 1247;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
