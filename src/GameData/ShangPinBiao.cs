using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShangPinBiao")]
	[Serializable]
	public class ShangPinBiao : IExtensible
	{
		private int _commodityId;

		private int _resId;

		private int _resNum;

		private int _unitPrice;

		private int _moneyType;

		private int _job;

		private int _pvpLevel;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "commodityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int commodityId
		{
			get
			{
				return this._commodityId;
			}
			set
			{
				this._commodityId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "resId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resId
		{
			get
			{
				return this._resId;
			}
			set
			{
				this._resId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "resNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resNum
		{
			get
			{
				return this._resNum;
			}
			set
			{
				this._resNum = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "unitPrice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int unitPrice
		{
			get
			{
				return this._unitPrice;
			}
			set
			{
				this._unitPrice = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "moneyType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int moneyType
		{
			get
			{
				return this._moneyType;
			}
			set
			{
				this._moneyType = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "job", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int job
		{
			get
			{
				return this._job;
			}
			set
			{
				this._job = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "pvpLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pvpLevel
		{
			get
			{
				return this._pvpLevel;
			}
			set
			{
				this._pvpLevel = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
