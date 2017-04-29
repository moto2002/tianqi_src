using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(773), ForSend(773), ProtoContract(Name = "EquipCompositeRes")]
	[Serializable]
	public class EquipCompositeRes : IExtensible
	{
		public static readonly short OP = 773;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
