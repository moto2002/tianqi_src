using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(451), ForSend(451), ProtoContract(Name = "MineInfoReq")]
	[Serializable]
	public class MineInfoReq : IExtensible
	{
		public static readonly short OP = 451;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
