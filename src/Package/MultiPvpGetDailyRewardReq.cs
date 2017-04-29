using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(188), ForSend(188), ProtoContract(Name = "MultiPvpGetDailyRewardReq")]
	[Serializable]
	public class MultiPvpGetDailyRewardReq : IExtensible
	{
		public static readonly short OP = 188;

		private int _rewardType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "rewardType", DataFormat = DataFormat.TwosComplement)]
		public int rewardType
		{
			get
			{
				return this._rewardType;
			}
			set
			{
				this._rewardType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
