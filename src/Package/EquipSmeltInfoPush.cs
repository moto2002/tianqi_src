using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7289), ForSend(7289), ProtoContract(Name = "EquipSmeltInfoPush")]
	[Serializable]
	public class EquipSmeltInfoPush : IExtensible
	{
		public static readonly short OP = 7289;

		private int _dayFund;

		private int _dayBuildTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "dayFund", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dayFund
		{
			get
			{
				return this._dayFund;
			}
			set
			{
				this._dayFund = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "dayBuildTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dayBuildTimes
		{
			get
			{
				return this._dayBuildTimes;
			}
			set
			{
				this._dayBuildTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
