using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "PetTalent")]
	[Serializable]
	public class PetTalent : IExtensible
	{
		public static readonly short OP = 392;

		private int _talentId;

		private int _talentLv;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "talentId", DataFormat = DataFormat.TwosComplement)]
		public int talentId
		{
			get
			{
				return this._talentId;
			}
			set
			{
				this._talentId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "talentLv", DataFormat = DataFormat.TwosComplement)]
		public int talentLv
		{
			get
			{
				return this._talentLv;
			}
			set
			{
				this._talentLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
