using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "DefendFightMode")]
	[Serializable]
	public class DefendFightMode : IExtensible
	{
		[ProtoContract(Name = "DFMD")]
		public enum DFMD
		{
			[ProtoEnum(Name = "Hold", Value = 1)]
			Hold = 1,
			[ProtoEnum(Name = "Protect", Value = 2)]
			Protect,
			[ProtoEnum(Name = "Save", Value = 3)]
			Save
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
