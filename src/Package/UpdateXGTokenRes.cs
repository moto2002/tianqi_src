using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(662), ForSend(662), ProtoContract(Name = "UpdateXGTokenRes")]
	[Serializable]
	public class UpdateXGTokenRes : IExtensible
	{
		public static readonly short OP = 662;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
