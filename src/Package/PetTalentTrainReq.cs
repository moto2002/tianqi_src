using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5013), ForSend(5013), ProtoContract(Name = "PetTalentTrainReq")]
	[Serializable]
	public class PetTalentTrainReq : IExtensible
	{
		public static readonly short OP = 5013;

		private long _petUUId;

		private int _talentId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petUUId", DataFormat = DataFormat.TwosComplement)]
		public long petUUId
		{
			get
			{
				return this._petUUId;
			}
			set
			{
				this._petUUId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "talentId", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
