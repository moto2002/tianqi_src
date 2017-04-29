using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(4328), ForSend(4328), ProtoContract(Name = "DailyTaskActivityNty")]
	[Serializable]
	public class DailyTaskActivityNty : IExtensible
	{
		public static readonly short OP = 4328;

		private int _totalActivity;

		private readonly List<int> _activityIds = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "totalActivity", DataFormat = DataFormat.TwosComplement)]
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
