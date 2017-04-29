using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XXiLianShuXingKu")]
	[Serializable]
	public class XXiLianShuXingKu : IExtensible
	{
		private int _attrId;

		private int _maxValue;

		private readonly List<float> _specialRange = new List<float>();

		private int _special;

		private readonly List<float> _normalRange = new List<float>();

		private int _weight;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "attrId", DataFormat = DataFormat.TwosComplement)]
		public int attrId
		{
			get
			{
				return this._attrId;
			}
			set
			{
				this._attrId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "maxValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxValue
		{
			get
			{
				return this._maxValue;
			}
			set
			{
				this._maxValue = value;
			}
		}

		[ProtoMember(4, Name = "specialRange", DataFormat = DataFormat.FixedSize)]
		public List<float> specialRange
		{
			get
			{
				return this._specialRange;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "special", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int special
		{
			get
			{
				return this._special;
			}
			set
			{
				this._special = value;
			}
		}

		[ProtoMember(6, Name = "normalRange", DataFormat = DataFormat.FixedSize)]
		public List<float> normalRange
		{
			get
			{
				return this._normalRange;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
