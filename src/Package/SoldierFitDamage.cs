using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "SoldierFitDamage")]
	[Serializable]
	public class SoldierFitDamage : IExtensible
	{
		private long _fitPetId;

		private int _totalDamage;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "fitPetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long fitPetId
		{
			get
			{
				return this._fitPetId;
			}
			set
			{
				this._fitPetId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "totalDamage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int totalDamage
		{
			get
			{
				return this._totalDamage;
			}
			set
			{
				this._totalDamage = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
