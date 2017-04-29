using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GouMaiJiaGe")]
	[Serializable]
	public class GouMaiJiaGe : IExtensible
	{
		private int _times;

		private int _price;

		private int _coin;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "times", DataFormat = DataFormat.TwosComplement)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "price", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int price
		{
			get
			{
				return this._price;
			}
			set
			{
				this._price = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "coin", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int coin
		{
			get
			{
				return this._coin;
			}
			set
			{
				this._coin = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
