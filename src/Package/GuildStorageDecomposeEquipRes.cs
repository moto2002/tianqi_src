using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3870), ForSend(3870), ProtoContract(Name = "GuildStorageDecomposeEquipRes")]
	[Serializable]
	public class GuildStorageDecomposeEquipRes : IExtensible
	{
		public static readonly short OP = 3870;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
