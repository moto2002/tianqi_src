using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ColorType")]
	[Serializable]
	public class ColorType : IExtensible
	{
		[ProtoContract(Name = "CT")]
		public enum CT
		{
			[ProtoEnum(Name = "WHITE", Value = 1)]
			WHITE = 1,
			[ProtoEnum(Name = "GREEN", Value = 2)]
			GREEN,
			[ProtoEnum(Name = "BLUE", Value = 3)]
			BLUE,
			[ProtoEnum(Name = "PURPLE", Value = 4)]
			PURPLE,
			[ProtoEnum(Name = "ORANGE", Value = 5)]
			ORANGE
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
