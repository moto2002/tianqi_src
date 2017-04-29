using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(878), ForSend(878), ProtoContract(Name = "ExitAreaBattleReq")]
	[Serializable]
	public class ExitAreaBattleReq : IExtensible
	{
		public static readonly short OP = 878;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
