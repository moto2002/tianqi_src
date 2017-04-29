using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TiLiGouMai")]
	[Serializable]
	public class TiLiGouMai : IExtensible
	{
		private int _buyTimes;

		private int _needDiamond;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "buyTimes", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "needDiamond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
