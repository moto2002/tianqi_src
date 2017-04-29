using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(190), ForSend(190), ProtoContract(Name = "BattleReliveReq")]
	[Serializable]
	public class BattleReliveReq : IExtensible
	{
		public static readonly short OP = 190;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
