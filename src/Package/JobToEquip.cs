using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "JobToEquip")]
	[Serializable]
	public class JobToEquip : IExtensible
	{
		private int _career;

		private EquipSimpleInfo _equip;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "career", DataFormat = DataFormat.TwosComplement)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "equip", DataFormat = DataFormat.Default)]
		public EquipSimpleInfo equip
		{
			get
			{
				return this._equip;
			}
			set
			{
				this._equip = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
