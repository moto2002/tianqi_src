using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5000), ForSend(5000), ProtoContract(Name = "GetHolyWeaponsInfoReq")]
	[Serializable]
	public class GetHolyWeaponsInfoReq : IExtensible
	{
		public static readonly short OP = 5000;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
