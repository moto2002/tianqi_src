using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChongWuTianFuZhanLi")]
	[Serializable]
	public class ChongWuTianFuZhanLi : IExtensible
	{
		private int _id;

		private int _petUnitFightPower;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "petUnitFightPower", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petUnitFightPower
		{
			get
			{
				return this._petUnitFightPower;
			}
			set
			{
				this._petUnitFightPower = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
