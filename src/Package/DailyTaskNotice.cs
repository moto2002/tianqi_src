using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(642), ForSend(642), ProtoContract(Name = "DailyTaskNotice")]
	[Serializable]
	public class DailyTaskNotice : IExtensible
	{
		public static readonly short OP = 642;

		private readonly List<DailyTask> _dailyTasks = new List<DailyTask>();

		private int _totalActivity;

		private readonly List<int> _activityIds = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "dailyTasks", DataFormat = DataFormat.Default)]
		public List<DailyTask> dailyTasks
		{
			get
			{
				return this._dailyTasks;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "totalActivity", DataFormat = DataFormat.TwosComplement)]
		public int totalActivity
		{
			get
			{
				return this._totalActivity;
			}
			set
			{
				this._totalActivity = value;
			}
		}

		[ProtoMember(3, Name = "activityIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> activityIds
		{
			get
			{
				return this._activityIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
