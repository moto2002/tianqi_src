using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1578), ForSend(1578), ProtoContract(Name = "SurvivalChallengeKillMonsterTimeNty")]
	[Serializable]
	public class SurvivalChallengeKillMonsterTimeNty : IExtensible
	{
		public static readonly short OP = 1578;

		private int _sec;

		private int _utcSec;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sec", DataFormat = DataFormat.TwosComplement)]
		public int sec
		{
			get
			{
				return this._sec;
			}
			set
			{
				this._sec = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "utcSec", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int utcSec
		{
			get
			{
				return this._utcSec;
			}
			set
			{
				this._utcSec = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
