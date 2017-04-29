using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BattleSkillAttrAdd")]
	[Serializable]
	public class BattleSkillAttrAdd : IExtensible
	{
		private int _attrType;

		private int _multiAdd;

		private int _addiAdd;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "attrType", DataFormat = DataFormat.TwosComplement)]
		public int attrType
		{
			get
			{
				return this._attrType;
			}
			set
			{
				this._attrType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "multiAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int multiAdd
		{
			get
			{
				return this._multiAdd;
			}
			set
			{
				this._multiAdd = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "addiAdd", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int addiAdd
		{
			get
			{
				return this._addiAdd;
			}
			set
			{
				this._addiAdd = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
