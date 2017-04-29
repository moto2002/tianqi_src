using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GongHuiShangPinBiao")]
	[Serializable]
	public class GongHuiShangPinBiao : IExtensible
	{
		private int _commodityId;

		private int _resId;

		private int _resNum;

		private int _unitPrice;

		private int _moneyType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "commodityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "resId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "resNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "unitPrice", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "moneyType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
