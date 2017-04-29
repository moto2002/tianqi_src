using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "PetFormations")]
	[Serializable]
	public class PetFormations : IExtensible
	{
		private readonly List<PetFormation> _petFormations = new List<PetFormation>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "petFormations", DataFormat = DataFormat.Default)]
		public List<PetFormation> petFormations
		{
			get
			{
				return this._petFormations;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
