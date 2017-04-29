using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3809), ForSend(3809), ProtoContract(Name = "BattleTimeInfoReq")]
	[Serializable]
	public class BattleTimeInfoReq : IExtensible
	{
		public static readonly short OP = 3809;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
