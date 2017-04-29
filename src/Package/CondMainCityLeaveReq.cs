using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(62), ForSend(62), ProtoContract(Name = "CondMainCityLeaveReq")]
	[Serializable]
	public class CondMainCityLeaveReq : IExtensible
	{
		public static readonly short OP = 62;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
