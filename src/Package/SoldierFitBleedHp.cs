using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "SoldierFitBleedHp")]
	[Serializable]
	public class SoldierFitBleedHp : IExtensible
	{
		private long _fitPetId;

		private int _totalBleedHp;

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

		[ProtoMember(2, IsRequired = false, Name = "totalBleedHp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int totalBleedHp
		{
			get
			{
				return this._totalBleedHp;
			}
			set
			{
				this._totalBleedHp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
