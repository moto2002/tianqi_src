using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "TestType")]
	[Serializable]
	public class TestType : IExtensible
	{
		[ProtoContract(Name = "TT")]
		public enum TT
		{
			[ProtoEnum(Name = "SET_ROLE_LV", Value = 1)]
			SET_ROLE_LV = 1,
			[ProtoEnum(Name = "SET_PET_LV", Value = 2)]
			SET_PET_LV,
			[ProtoEnum(Name = "ADD_EXP", Value = 3)]
			ADD_EXP,
			[ProtoEnum(Name = "USE_ITEM", Value = 4)]
			USE_ITEM,
			[ProtoEnum(Name = "ENTER_COPY", Value = 5)]
			ENTER_COPY,
			[ProtoEnum(Name = "ClI_DRV_COPY_ST", Value = 6)]
			ClI_DRV_COPY_ST
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
