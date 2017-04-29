using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(778), ForSend(778), ProtoContract(Name = "ActivityLoginPush")]
	[Serializable]
	public class ActivityLoginPush : IExtensible
	{
		public static readonly short OP = 778;

		private readonly List<ActivityInfo> _activitiesInfo = new List<ActivityInfo>();

		private int _startDay;

		private readonly List<int> _endTimeouts = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "activitiesInfo", DataFormat = DataFormat.Default)]
		public List<ActivityInfo> activitiesInfo
		{
			get
			{
				return this._activitiesInfo;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "startDay", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int startDay
		{
			get
			{
				return this._startDay;
			}
			set
			{
				this._startDay = value;
			}
		}

		[ProtoMember(3, Name = "endTimeouts", DataFormat = DataFormat.TwosComplement)]
		public List<int> endTimeouts
		{
			get
			{
				return this._endTimeouts;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
