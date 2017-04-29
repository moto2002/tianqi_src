using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "KVType")]
	[Serializable]
	public class KVType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "TransformId", Value = 0)]
			TransformId
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
