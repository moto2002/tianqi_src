using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GemSlotOpen")]
	[Serializable]
	public class GemSlotOpen : IExtensible
	{
		private EquipLibType.ELT _type;

		private int _hole;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public EquipLibType.ELT type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "hole", DataFormat = DataFormat.TwosComplement)]
		public int hole
		{
			get
			{
				return this._hole;
			}
			set
			{
				this._hole = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
