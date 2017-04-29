using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_PetLeaveField")]
	[Serializable]
	public class BattleAction_PetLeaveField : IExtensible
	{
		private long _petId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public long petId
		{
			get
			{
				return this._petId;
			}
			set
			{
				this._petId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
