using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(615), ForSend(615), ProtoContract(Name = "ExitWildBossReq")]
	[Serializable]
	public class ExitWildBossReq : IExtensible
	{
		public static readonly short OP = 615;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
