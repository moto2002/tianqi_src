using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "WildBossUpdateType")]
	[Serializable]
	public class WildBossUpdateType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Status", Value = 1)]
			Status = 1
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
