using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "Tab")]
	[Serializable]
	public class Tab : IExtensible
	{
		[ProtoContract(Name = "TAB")]
		public enum TAB
		{
			[ProtoEnum(Name = "RoleGrow", Value = 1)]
			RoleGrow = 1,
			[ProtoEnum(Name = "PlayPass", Value = 2)]
			PlayPass,
			[ProtoEnum(Name = "EquipForming", Value = 3)]
			EquipForming,
			[ProtoEnum(Name = "PetTrain", Value = 4)]
			PetTrain
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
