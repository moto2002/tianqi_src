using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(917), ForSend(917), ProtoContract(Name = "WildBossRewardInfoNty")]
	[Serializable]
	public class WildBossRewardInfoNty : IExtensible
	{
		public static readonly short OP = 917;

		private int _singleRewardTms;

		private int _teamRewardTms;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "singleRewardTms", DataFormat = DataFormat.TwosComplement)]
		public int singleRewardTms
		{
			get
			{
				return this._singleRewardTms;
			}
			set
			{
				this._singleRewardTms = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "teamRewardTms", DataFormat = DataFormat.TwosComplement)]
		public int teamRewardTms
		{
			get
			{
				return this._teamRewardTms;
			}
			set
			{
				this._teamRewardTms = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
