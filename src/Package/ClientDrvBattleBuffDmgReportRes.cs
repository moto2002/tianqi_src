using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1318), ForSend(1318), ProtoContract(Name = "ClientDrvBattleBuffDmgReportRes")]
	[Serializable]
	public class ClientDrvBattleBuffDmgReportRes : IExtensible
	{
		public static readonly short OP = 1318;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
