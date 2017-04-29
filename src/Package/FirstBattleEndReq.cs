using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(119), ForSend(119), ProtoContract(Name = "FirstBattleEndReq")]
	[Serializable]
	public class FirstBattleEndReq : IExtensible
	{
		public static readonly short OP = 119;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
