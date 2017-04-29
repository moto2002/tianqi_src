using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2992), ForSend(2992), ProtoContract(Name = "AdvancedOccupationRes")]
	[Serializable]
	public class AdvancedOccupationRes : IExtensible
	{
		public static readonly short OP = 2992;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
