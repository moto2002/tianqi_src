using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(8212), ForSend(8212), ProtoContract(Name = "TaskDataNty")]
	[Serializable]
	public class TaskDataNty : IExtensible
	{
		public static readonly short OP = 8212;

		private int _taskId;

		private int _extraData;

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

		[ProtoMember(2, IsRequired = false, Name = "extraData", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int extraData
		{
			get
			{
				return this._extraData;
			}
			set
			{
				this._extraData = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
