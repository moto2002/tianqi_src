using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GMCommandType")]
	[Serializable]
	public class GMCommandType : IExtensible
	{
		[ProtoContract(Name = "GCT")]
		public enum GCT
		{
			[ProtoEnum(Name = "Mail", Value = 1)]
			Mail = 1,
			[ProtoEnum(Name = "Shutup", Value = 2)]
			Shutup,
			[ProtoEnum(Name = "Kickout", Value = 3)]
			Kickout
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
