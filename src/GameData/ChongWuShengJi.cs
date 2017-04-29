using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChongWuShengJi")]
	[Serializable]
	public class ChongWuShengJi : IExtensible
	{
		private int _PetLv;

		private int _Experience;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "PetLv", DataFormat = DataFormat.TwosComplement)]
		public int PetLv
		{
			get
			{
				return this._PetLv;
			}
			set
			{
				this._PetLv = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "Experience", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Experience
		{
			get
			{
				return this._Experience;
			}
			set
			{
				this._Experience = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
