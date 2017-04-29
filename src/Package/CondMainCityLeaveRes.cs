using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(63), ForSend(63), ProtoContract(Name = "CondMainCityLeaveRes")]
	[Serializable]
	public class CondMainCityLeaveRes : IExtensible
	{
		public static readonly short OP = 63;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
