using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "damageIncrease")]
	[Serializable]
	public class damageIncrease : IExtensible
	{
		private int _id;

		private int _targetSkill;

		private int _attrType;

		private int _Value1;

		private int _Value2;

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

		[ProtoMember(3, IsRequired = false, Name = "targetSkill", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetSkill
		{
			get
			{
				return this._targetSkill;
			}
			set
			{
				this._targetSkill = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "attrType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "Value1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Value1
		{
			get
			{
				return this._Value1;
			}
			set
			{
				this._Value1 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "Value2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Value2
		{
			get
			{
				return this._Value2;
			}
			set
			{
				this._Value2 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
