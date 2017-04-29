using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1084), ForSend(1084), ProtoContract(Name = "acceptAchievementAwardReq")]
	[Serializable]
	public class acceptAchievementAwardReq : IExtensible
	{
		public static readonly short OP = 1084;

		private int _achievementId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "achievementId", DataFormat = DataFormat.TwosComplement)]
		public int achievementId
		{
			get
			{
				return this._achievementId;
			}
			set
			{
				this._achievementId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
