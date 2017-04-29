using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "HitEventType")]
	[Serializable]
	public class HitEventType : IExtensible
	{
		[ProtoContract(Name = "HET")]
		public enum HET
		{
			[ProtoEnum(Name = "Common", Value = 0)]
			Common,
			[ProtoEnum(Name = "Player", Value = 1)]
			Player,
			[ProtoEnum(Name = "Item", Value = 2)]
			Item,
			[ProtoEnum(Name = "InterFace", Value = 3)]
			InterFace,
			[ProtoEnum(Name = "JumpLink", Value = 4)]
			JumpLink
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
