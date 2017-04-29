using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(961), ForSend(961), ProtoContract(Name = "PveDungeonDataErrorNty")]
	[Serializable]
	public class PveDungeonDataErrorNty : IExtensible
	{
		public static readonly short OP = 961;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
