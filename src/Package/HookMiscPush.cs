using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1857), ForSend(1857), ProtoContract(Name = "HookMiscPush")]
	[Serializable]
	public class HookMiscPush : IExtensible
	{
		public static readonly short OP = 1857;

		private int _remainTimeSec;

		private int _dayBuyTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "remainTimeSec", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainTimeSec
		{
			get
			{
				return this._remainTimeSec;
			}
			set
			{
				this._remainTimeSec = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "dayBuyTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dayBuyTimes
		{
			get
			{
				return this._dayBuyTimes;
			}
			set
			{
				this._dayBuyTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
