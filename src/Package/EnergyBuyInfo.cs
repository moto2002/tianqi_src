using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "EnergyBuyInfo")]
	[Serializable]
	public class EnergyBuyInfo : IExtensible
	{
		private int _buyTimes;

		private int _maxBuyTimes;

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

		[ProtoMember(2, IsRequired = true, Name = "maxBuyTimes", DataFormat = DataFormat.TwosComplement)]
		public int maxBuyTimes
		{
			get
			{
				return this._maxBuyTimes;
			}
			set
			{
				this._maxBuyTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
