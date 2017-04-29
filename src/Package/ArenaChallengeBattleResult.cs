using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1115), ForSend(1115), ProtoContract(Name = "ArenaChallengeBattleResult")]
	[Serializable]
	public class ArenaChallengeBattleResult : IExtensible
	{
		[ProtoContract(Name = "BattleResult")]
		public enum BattleResult
		{
			[ProtoEnum(Name = "Win", Value = 0)]
			Win,
			[ProtoEnum(Name = "Fail", Value = 1)]
			Fail,
			[ProtoEnum(Name = "Even", Value = 2)]
			Even
		}

		public static readonly short OP = 1115;

		private ArenaChallengeBattleResult.BattleResult _status;

		private int _oldScore;

		private int _gainScore;

		private int _newScore;

		private int _combatWinCount;

		private int _competitiveCurrency;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public ArenaChallengeBattleResult.BattleResult status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "oldScore", DataFormat = DataFormat.TwosComplement)]
		public int oldScore
		{
			get
			{
				return this._oldScore;
			}
			set
			{
				this._oldScore = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "gainScore", DataFormat = DataFormat.TwosComplement)]
		public int gainScore
		{
			get
			{
				return this._gainScore;
			}
			set
			{
				this._gainScore = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "newScore", DataFormat = DataFormat.TwosComplement)]
		public int newScore
		{
			get
			{
				return this._newScore;
			}
			set
			{
				this._newScore = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "combatWinCount", DataFormat = DataFormat.TwosComplement)]
		public int combatWinCount
		{
			get
			{
				return this._combatWinCount;
			}
			set
			{
				this._combatWinCount = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "competitiveCurrency", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int competitiveCurrency
		{
			get
			{
				return this._competitiveCurrency;
			}
			set
			{
				this._competitiveCurrency = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
