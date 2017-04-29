using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7821), ForSend(7821), ProtoContract(Name = "AcceptTaskReq")]
	[Serializable]
	public class AcceptTaskReq : IExtensible
	{
		public static readonly short OP = 7821;

		private int _taskId;

		private int _taskNumber;

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

		[ProtoMember(2, IsRequired = false, Name = "taskNumber", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskNumber
		{
			get
			{
				return this._taskNumber;
			}
			set
			{
				this._taskNumber = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
