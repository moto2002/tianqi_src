using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(598), ForSend(598), ProtoContract(Name = "EvacuatePetRes")]
	[Serializable]
	public class EvacuatePetRes : IExtensible
	{
		public static readonly short OP = 598;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
