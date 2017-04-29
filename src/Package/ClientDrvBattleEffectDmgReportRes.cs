using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1402), ForSend(1402), ProtoContract(Name = "ClientDrvBattleEffectDmgReportRes")]
	[Serializable]
	public class ClientDrvBattleEffectDmgReportRes : IExtensible
	{
		public static readonly short OP = 1402;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
