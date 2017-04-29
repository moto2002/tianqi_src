using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(708), ForSend(708), ProtoContract(Name = "GoldBuyChangedNty")]
	[Serializable]
	public class GoldBuyChangedNty : IExtensible
	{
		public static readonly short OP = 708;

		private int _remainingBuyTimes;

		private int _remainingFreeTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "remainingBuyTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainingBuyTimes
		{
			get
			{
				return this._remainingBuyTimes;
			}
			set
			{
				this._remainingBuyTimes = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "remainingFreeTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainingFreeTimes
		{
			get
			{
				return this._remainingFreeTimes;
			}
			set
			{
				this._remainingFreeTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
