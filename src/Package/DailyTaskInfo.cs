using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "DailyTaskInfo")]
	[Serializable]
	public class DailyTaskInfo : IExtensible
	{
		private readonly List<DailyTask> _dailyTasks = new List<DailyTask>();

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

		[ProtoMember(2, Name = "activityIds", DataFormat = DataFormat.TwosComplement)]
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
