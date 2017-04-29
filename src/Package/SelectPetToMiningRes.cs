using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1752), ForSend(1752), ProtoContract(Name = "SelectPetToMiningRes")]
	[Serializable]
	public class SelectPetToMiningRes : IExtensible
	{
		public static readonly short OP = 1752;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
