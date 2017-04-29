using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(223), ForSend(223), ProtoContract(Name = "PveExitBattleReq")]
	[Serializable]
	public class PveExitBattleReq : IExtensible
	{
		public static readonly short OP = 223;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
