using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "OtherData")]
	[Serializable]
	public class OtherData : IExtensible
	{
		private int _taskType;

		private int _taskTimes;

		private long _lastUpdate;

		private int _lastVipAddTimes;

		private int _freeRefreshTimes;

		private int _nextFreshTime = -1;

		private int _lastRecordTime = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "taskType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskType
		{
			get
			{
				return this._taskType;
			}
			set
			{
				this._taskType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "taskTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskTimes
		{
			get
			{
				return this._taskTimes;
			}
			set
			{
				this._taskTimes = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "lastUpdate", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long lastUpdate
		{
			get
			{
				return this._lastUpdate;
			}
			set
			{
				this._lastUpdate = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "lastVipAddTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastVipAddTimes
		{
			get
			{
				return this._lastVipAddTimes;
			}
			set
			{
				this._lastVipAddTimes = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "freeRefreshTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int freeRefreshTimes
		{
			get
			{
				return this._freeRefreshTimes;
			}
			set
			{
				this._freeRefreshTimes = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "nextFreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int nextFreshTime
		{
			get
			{
				return this._nextFreshTime;
			}
			set
			{
				this._nextFreshTime = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "lastRecordTime", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int lastRecordTime
		{
			get
			{
				return this._lastRecordTime;
			}
			set
			{
				this._lastRecordTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
