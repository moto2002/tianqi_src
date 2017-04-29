using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ExtraRewardInfo")]
	[Serializable]
	public class ExtraRewardInfo : IExtensible
	{
		private int _taskType;

		private int _commitTimes;

		private readonly List<int> _gotTimesPoint = new List<int>();

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

		[ProtoMember(2, IsRequired = false, Name = "commitTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int commitTimes
		{
			get
			{
				return this._commitTimes;
			}
			set
			{
				this._commitTimes = value;
			}
		}

		[ProtoMember(3, Name = "gotTimesPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> gotTimesPoint
		{
			get
			{
				return this._gotTimesPoint;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
