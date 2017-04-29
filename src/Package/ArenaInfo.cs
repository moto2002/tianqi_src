using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(362), ForSend(362), ProtoContract(Name = "ArenaInfo")]
	[Serializable]
	public class ArenaInfo : IExtensible
	{
		public static readonly short OP = 362;

		private int _rank;

		private int _score;

		private int _totalWinCount;

		private int _combatWinCount;

		private int _rewardNum;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "rank", DataFormat = DataFormat.TwosComplement)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "score", DataFormat = DataFormat.TwosComplement)]
		public int score
		{
			get
			{
				return this._score;
			}
			set
			{
				this._score = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "totalWinCount", DataFormat = DataFormat.TwosComplement)]
		public int totalWinCount
		{
			get
			{
				return this._totalWinCount;
			}
			set
			{
				this._totalWinCount = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "combatWinCount", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = false, Name = "rewardNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardNum
		{
			get
			{
				return this._rewardNum;
			}
			set
			{
				this._rewardNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
