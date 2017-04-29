using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7023), ForSend(7023), ProtoContract(Name = "GetOpenServerBoxReq")]
	[Serializable]
	public class GetOpenServerBoxReq : IExtensible
	{
		public static readonly short OP = 7023;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
