using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(727), ForSend(727), ProtoContract(Name = "PetFormationPush")]
	[Serializable]
	public class PetFormationPush : IExtensible
	{
		public static readonly short OP = 727;

		private readonly List<PetFormation> _formations = new List<PetFormation>();

		private int _formationId;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "formations", DataFormat = DataFormat.Default)]
		public List<PetFormation> formations
		{
			get
			{
				return this._formations;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "formationId", DataFormat = DataFormat.TwosComplement)]
		public int formationId
		{
			get
			{
				return this._formationId;
			}
			set
			{
				this._formationId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
