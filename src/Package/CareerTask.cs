using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3218), ForSend(3218), ProtoContract(Name = "CareerTask")]
	[Serializable]
	public class CareerTask : IExtensible
	{
		[ProtoContract(Name = "TaskStatus")]
		public enum TaskStatus
		{
			[ProtoEnum(Name = "TaskReceived", Value = 1)]
			TaskReceived = 1,
			[ProtoEnum(Name = "WaittingCommit", Value = 2)]
			WaittingCommit,
			[ProtoEnum(Name = "Finish", Value = 3)]
			Finish
		}

		public static readonly short OP = 3218;

		private int _taskId;

		private int _count;

		private CareerTask.TaskStatus _status = CareerTask.TaskStatus.TaskReceived;

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

		[ProtoMember(2, IsRequired = true, Name = "count", DataFormat = DataFormat.TwosComplement)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "status", DataFormat = DataFormat.TwosComplement), DefaultValue(CareerTask.TaskStatus.TaskReceived)]
		public CareerTask.TaskStatus status
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
