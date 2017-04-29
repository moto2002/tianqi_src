using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "RoleGenderType")]
	[Serializable]
	public class RoleGenderType : IExtensible
	{
		[ProtoContract(Name = "GENDER_TYPE")]
		public enum GENDER_TYPE
		{
			[ProtoEnum(Name = "MALE", Value = 0)]
			MALE,
			[ProtoEnum(Name = "FEMALE", Value = 1)]
			FEMALE
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
