using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(853), ForSend(853), ProtoContract(Name = "GetDailyTaskPrizeReq")]
	[Serializable]
	public class GetDailyTaskPrizeReq : IExtensible
	{
		public static readonly short OP = 853;

		private int _taskId;

		private int _opType;

		private int _findTimes = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "taskId", DataFormat = DataFormat.TwosComplement)]
		public int taskId
		{
			get
			{
				return this._taskId;
			}
			set
			{
				this._taskId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "opType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int opType
		{
			get
			{
				return this._opType;
			}
			set
			{
				this._opType = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "findTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int findTimes
		{
			get
			{
				return this._findTimes;
			}
			set
			{
				this._findTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
