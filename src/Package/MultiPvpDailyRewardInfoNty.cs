using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(184), ForSend(184), ProtoContract(Name = "MultiPvpDailyRewardInfoNty")]
	[Serializable]
	public class MultiPvpDailyRewardInfoNty : IExtensible
	{
		[ProtoContract(Name = "MultiPvpRewardInfo")]
		[Serializable]
		public class MultiPvpRewardInfo : IExtensible
		{
			private int _rewardId;

			private bool _getFlag;

			private int _process;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "rewardId", DataFormat = DataFormat.TwosComplement)]
			public int rewardId
			{
				get
				{
					return this._rewardId;
				}
				set
				{
					this._rewardId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "getFlag", DataFormat = DataFormat.Default)]
			public bool getFlag
			{
				get
				{
					return this._getFlag;
				}
				set
				{
					this._getFlag = value;
				}
			}

			[ProtoMember(3, IsRequired = true, Name = "process", DataFormat = DataFormat.TwosComplement)]
			public int process
			{
				get
				{
					return this._process;
				}
				set
				{
					this._process = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 184;

		private readonly List<MultiPvpDailyRewardInfoNty.MultiPvpRewardInfo> _dailyReward = new List<MultiPvpDailyRewardInfoNty.MultiPvpRewardInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "dailyReward", DataFormat = DataFormat.Default)]
		public List<MultiPvpDailyRewardInfoNty.MultiPvpRewardInfo> dailyReward
		{
			get
			{
				return this._dailyReward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
