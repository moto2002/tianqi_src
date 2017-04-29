using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "TargetTaskInfo")]
	[Serializable]
	public class TargetTaskInfo : IExtensible
	{
		[ProtoContract(Name = "GetRewardStatus")]
		public enum GetRewardStatus
		{
			[ProtoEnum(Name = "Available", Value = 1)]
			Available = 1,
			[ProtoEnum(Name = "HadGet", Value = 2)]
			HadGet,
			[ProtoEnum(Name = "Unavailable", Value = 3)]
			Unavailable,
			[ProtoEnum(Name = "NotGet", Value = 4)]
			NotGet
		}

		private int _targetID;

		private TargetTaskInfo.GetRewardStatus _status;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "targetID", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public TargetTaskInfo.GetRewardStatus status
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
