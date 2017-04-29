using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1065), ForSend(1065), ProtoContract(Name = "GetOpenServerActRewardReq")]
	[Serializable]
	public class GetOpenServerActRewardReq : IExtensible
	{
		public static readonly short OP = 1065;

		private OpenServerType.acType _activityType;

		private int _targetID;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "activityType", DataFormat = DataFormat.TwosComplement)]
		public OpenServerType.acType activityType
		{
			get
			{
				return this._activityType;
			}
			set
			{
				this._activityType = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "targetID", DataFormat = DataFormat.TwosComplement)]
		public int targetID
		{
			get
			{
				return this._targetID;
			}
			set
			{
				this._targetID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
