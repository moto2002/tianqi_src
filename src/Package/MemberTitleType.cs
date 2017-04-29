using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "MemberTitleType")]
	[Serializable]
	public class MemberTitleType : IExtensible
	{
		[ProtoContract(Name = "MTT")]
		public enum MTT
		{
			[ProtoEnum(Name = "Normal", Value = 1)]
			Normal = 1,
			[ProtoEnum(Name = "Chairman", Value = 2)]
			Chairman,
			[ProtoEnum(Name = "ViceChairman", Value = 3)]
			ViceChairman,
			[ProtoEnum(Name = "Manager", Value = 4)]
			Manager,
			[ProtoEnum(Name = "ImageAmbassador", Value = 10)]
			ImageAmbassador = 10,
			[ProtoEnum(Name = "StrongMan", Value = 11)]
			StrongMan,
			[ProtoEnum(Name = "Warden", Value = 12)]
			Warden,
			[ProtoEnum(Name = "RichMan", Value = 13)]
			RichMan
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
