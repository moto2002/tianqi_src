using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(734), ForSend(734), ProtoContract(Name = "PlanLightPointReq")]
	[Serializable]
	public class PlanLightPointReq : IExtensible
	{
		public static readonly short OP = 734;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
