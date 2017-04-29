using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ItemOperation")]
	[Serializable]
	public class ItemOperation : IExtensible
	{
		[ProtoContract(Name = "IO")]
		public enum IO
		{
			[ProtoEnum(Name = "Add", Value = 1)]
			Add = 1,
			[ProtoEnum(Name = "Modify", Value = 2)]
			Modify,
			[ProtoEnum(Name = "Del", Value = 3)]
			Del
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
