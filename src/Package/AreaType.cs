using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "AreaType")]
	[Serializable]
	public class AreaType : IExtensible
	{
		[ProtoContract(Name = "AT")]
		public enum AT
		{
			[ProtoEnum(Name = "Safe", Value = 1)]
			Safe = 1,
			[ProtoEnum(Name = "Chaos", Value = 2)]
			Chaos,
			[ProtoEnum(Name = "Vip", Value = 3)]
			Vip
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
