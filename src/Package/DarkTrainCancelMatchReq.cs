using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2107), ForSend(2107), ProtoContract(Name = "DarkTrainCancelMatchReq")]
	[Serializable]
	public class DarkTrainCancelMatchReq : IExtensible
	{
		public static readonly short OP = 2107;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
