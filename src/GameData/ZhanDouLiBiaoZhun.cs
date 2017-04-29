using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhanDouLiBiaoZhun")]
	[Serializable]
	public class ZhanDouLiBiaoZhun : IExtensible
	{
		private int _id;

		private int _roleFightPower;

		private int _petFightPower;

		private int _equitFightPower;

		private float _unit;

		private float _unitFightPower;

		private float _attrsAdjustment;

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

		[ProtoMember(5, IsRequired = false, Name = "roleFightPower", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int roleFightPower
		{
			get
			{
				return this._roleFightPower;
			}
			set
			{
				this._roleFightPower = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "petFightPower", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petFightPower
		{
			get
			{
				return this._petFightPower;
			}
			set
			{
				this._petFightPower = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "equitFightPower", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int equitFightPower
		{
			get
			{
				return this._equitFightPower;
			}
			set
			{
				this._equitFightPower = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "unit", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				this._unit = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "unitFightPower", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float unitFightPower
		{
			get
			{
				return this._unitFightPower;
			}
			set
			{
				this._unitFightPower = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "attrsAdjustment", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float attrsAdjustment
		{
			get
			{
				return this._attrsAdjustment;
			}
			set
			{
				this._attrsAdjustment = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
