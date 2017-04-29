using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "BuddyPetFormation")]
	[Serializable]
	public class BuddyPetFormation : IExtensible
	{
		private PetFormationType.FORMATION_TYPE _fType;

		private readonly List<PetInfo> _petInfo = new List<PetInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "fType", DataFormat = DataFormat.TwosComplement)]
		public PetFormationType.FORMATION_TYPE fType
		{
			get
			{
				return this._fType;
			}
			set
			{
				this._fType = value;
			}
		}

		[ProtoMember(2, Name = "petInfo", DataFormat = DataFormat.Default)]
		public List<PetInfo> petInfo
		{
			get
			{
				return this._petInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
