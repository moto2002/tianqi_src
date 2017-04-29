using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "AutoMatchType")]
	[Serializable]
	public class AutoMatchType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "Rule1", Value = 1)]
			Rule1 = 1,
			[ProtoEnum(Name = "Rule2", Value = 10)]
			Rule2 = 10
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
