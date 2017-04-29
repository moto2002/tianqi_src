using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BountyTaskType")]
	[Serializable]
	public class BountyTaskType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Normal", Value = 1)]
			Normal = 1,
			[ProtoEnum(Name = "Urgent", Value = 2)]
			Urgent
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
