using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(850), ForSend(850), ProtoContract(Name = "LightUpEquipmentRes")]
	[Serializable]
	public class LightUpEquipmentRes : IExtensible
	{
		public static readonly short OP = 850;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
