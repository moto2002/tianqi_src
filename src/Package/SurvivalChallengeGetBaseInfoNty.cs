using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(203), ForSend(203), ProtoContract(Name = "SurvivalChallengeGetBaseInfoNty")]
	[Serializable]
	public class SurvivalChallengeGetBaseInfoNty : IExtensible
	{
		public static readonly short OP = 203;

		private int _challengedTimes;

		private int _challengeCostTime;

		private readonly List<ScDungeonInfo> _dungeonInfos = new List<ScDungeonInfo>();

		private int _maxCanChallengeDifficulty;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "challengedTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int challengedTimes
		{
			get
			{
				return this._challengedTimes;
			}
			set
			{
				this._challengedTimes = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "challengeCostTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int challengeCostTime
		{
			get
			{
				return this._challengeCostTime;
			}
			set
			{
				this._challengeCostTime = value;
			}
		}

		[ProtoMember(3, Name = "dungeonInfos", DataFormat = DataFormat.Default)]
		public List<ScDungeonInfo> dungeonInfos
		{
			get
			{
				return this._dungeonInfos;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "maxCanChallengeDifficulty", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxCanChallengeDifficulty
		{
			get
			{
				return this._maxCanChallengeDifficulty;
			}
			set
			{
				this._maxCanChallengeDifficulty = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
