using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JinBiGouMai")]
	[Serializable]
	public class JinBiGouMai : IExtensible
	{
		private int _buyTimes;

		private int _needDiamond;

		private int _gainGold;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "buyTimes", DataFormat = DataFormat.TwosComplement)]
		public int buyTimes
		{
			get
			{
				return this._buyTimes;
			}
			set
			{
				this._buyTimes = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "needDiamond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int needDiamond
		{
			get
			{
				return this._needDiamond;
			}
			set
			{
				this._needDiamond = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "gainGold", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gainGold
		{
			get
			{
				return this._gainGold;
			}
			set
			{
				this._gainGold = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
