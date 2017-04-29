using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(210), ForSend(210), ProtoContract(Name = "CancelAutoMatchReq")]
	[Serializable]
	public class CancelAutoMatchReq : IExtensible
	{
		public static readonly short OP = 210;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
