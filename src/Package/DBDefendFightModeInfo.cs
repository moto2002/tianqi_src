using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBDefendFightModeInfo")]
	[Serializable]
	public class DBDefendFightModeInfo : IExtensible
	{
		private DefendFightMode.DFMD _mode;

		private readonly List<int> _dungeonIds = new List<int>();

		private int _todayChallengeTimes;

		private int _todayBuyTimes;

		private int _lastResetTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "mode", DataFormat = DataFormat.TwosComplement)]
		public DefendFightMode.DFMD mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		[ProtoMember(2, Name = "dungeonIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> dungeonIds
		{
			get
			{
				return this._dungeonIds;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "todayChallengeTimes", DataFormat = DataFormat.TwosComplement)]
		public int todayChallengeTimes
		{
			get
			{
				return this._todayChallengeTimes;
			}
			set
			{
				this._todayChallengeTimes = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "todayBuyTimes", DataFormat = DataFormat.TwosComplement)]
		public int todayBuyTimes
		{
			get
			{
				return this._todayBuyTimes;
			}
			set
			{
				this._todayBuyTimes = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "lastResetTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastResetTime
		{
			get
			{
				return this._lastResetTime;
			}
			set
			{
				this._lastResetTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
