using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(140), ForSend(140), ProtoContract(Name = "MonthCardTimeReq")]
	[Serializable]
	public class MonthCardTimeReq : IExtensible
	{
		public static readonly short OP = 140;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
