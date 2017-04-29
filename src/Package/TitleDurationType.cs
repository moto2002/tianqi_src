using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "TitleDurationType")]
	[Serializable]
	public class TitleDurationType : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "TimeLimit", Value = 1)]
			TimeLimit = 1,
			[ProtoEnum(Name = "Temporary", Value = 2)]
			Temporary,
			[ProtoEnum(Name = "Forever", Value = 3)]
			Forever
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
