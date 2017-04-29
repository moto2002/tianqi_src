using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(152), ForSend(152), ProtoContract(Name = "GetOpenServerActInfoRes")]
	[Serializable]
	public class GetOpenServerActInfoRes : IExtensible
	{
		public static readonly short OP = 152;

		private OpenServerType.acType _activityType;

		private int _myRankNum;

		private readonly List<TargetTaskInfo> _targetInfos = new List<TargetTaskInfo>();

		private readonly List<RankingRoleInfo> _role = new List<RankingRoleInfo>();

		private long _myScore;

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

		[ProtoMember(2, IsRequired = false, Name = "myRankNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int myRankNum
		{
			get
			{
				return this._myRankNum;
			}
			set
			{
				this._myRankNum = value;
			}
		}

		[ProtoMember(3, Name = "targetInfos", DataFormat = DataFormat.Default)]
		public List<TargetTaskInfo> targetInfos
		{
			get
			{
				return this._targetInfos;
			}
		}

		[ProtoMember(4, Name = "role", DataFormat = DataFormat.Default)]
		public List<RankingRoleInfo> role
		{
			get
			{
				return this._role;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "myScore", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long myScore
		{
			get
			{
				return this._myScore;
			}
			set
			{
				this._myScore = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
