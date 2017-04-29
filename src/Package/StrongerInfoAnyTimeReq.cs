using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(967), ForSend(967), ProtoContract(Name = "StrongerInfoAnyTimeReq")]
	[Serializable]
	public class StrongerInfoAnyTimeReq : IExtensible
	{
		public static readonly short OP = 967;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
