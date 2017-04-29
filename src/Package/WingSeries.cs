using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "WingSeries")]
	[Serializable]
	public class WingSeries : IExtensible
	{
		[ProtoContract(Name = "WS")]
		public enum WS
		{
			[ProtoEnum(Name = "MACHINE", Value = 1)]
			MACHINE = 1,
			[ProtoEnum(Name = "GHOST", Value = 2)]
			GHOST,
			[ProtoEnum(Name = "BIOLOGY", Value = 3)]
			BIOLOGY
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
