using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2104), ForSend(2104), ProtoContract(Name = "DarkTrainAutoMatchReq")]
	[Serializable]
	public class DarkTrainAutoMatchReq : IExtensible
	{
		public static readonly short OP = 2104;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
