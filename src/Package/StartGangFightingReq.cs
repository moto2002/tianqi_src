using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(859), ForSend(859), ProtoContract(Name = "StartGangFightingReq")]
	[Serializable]
	public class StartGangFightingReq : IExtensible
	{
		public static readonly short OP = 859;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
