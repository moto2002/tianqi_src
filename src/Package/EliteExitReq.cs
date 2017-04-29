using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5412), ForSend(5412), ProtoContract(Name = "EliteExitReq")]
	[Serializable]
	public class EliteExitReq : IExtensible
	{
		public static readonly short OP = 5412;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
