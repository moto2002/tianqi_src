using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "BUFFXiaoHao")]
	[Serializable]
	public class BUFFXiaoHao : IExtensible
	{
		private int _dailyNum;

		private int _currencyType;

		private int _currencyNum;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "dailyNum", DataFormat = DataFormat.TwosComplement)]
		public int dailyNum
		{
			get
			{
				return this._dailyNum;
			}
			set
			{
				this._dailyNum = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "currencyType", DataFormat = DataFormat.TwosComplement)]
		public int currencyType
		{
			get
			{
				return this._currencyType;
			}
			set
			{
				this._currencyType = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "currencyNum", DataFormat = DataFormat.TwosComplement)]
		public int currencyNum
		{
			get
			{
				return this._currencyNum;
			}
			set
			{
				this._currencyNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
