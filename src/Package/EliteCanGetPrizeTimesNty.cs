using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(110), ForSend(110), ProtoContract(Name = "EliteCanGetPrizeTimesNty")]
	[Serializable]
	public class EliteCanGetPrizeTimesNty : IExtensible
	{
		public static readonly short OP = 110;

		private int _remainTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "remainTimes", DataFormat = DataFormat.TwosComplement)]
		public int remainTimes
		{
			get
			{
				return this._remainTimes;
			}
			set
			{
				this._remainTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
