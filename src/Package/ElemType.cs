using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ElemType")]
	[Serializable]
	public class ElemType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Normal", Value = 0)]
			Normal,
			[ProtoEnum(Name = "Earth", Value = 1)]
			Earth,
			[ProtoEnum(Name = "Fire", Value = 2)]
			Fire,
			[ProtoEnum(Name = "Water", Value = 3)]
			Water,
			[ProtoEnum(Name = "Thunder", Value = 4)]
			Thunder
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
