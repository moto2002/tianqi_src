using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1067), ForSend(1067), ProtoContract(Name = "GetOpenServerActRewardRes")]
	[Serializable]
	public class GetOpenServerActRewardRes : IExtensible
	{
		public static readonly short OP = 1067;

		private OpenServerType.acType _activityType;

		private int _targetID;

		private readonly List<ItemBriefInfo> _rewards = new List<ItemBriefInfo>();

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

		[ProtoMember(3, Name = "rewards", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> rewards
		{
			get
			{
				return this._rewards;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
