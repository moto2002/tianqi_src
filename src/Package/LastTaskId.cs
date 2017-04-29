using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "LastTaskId")]
	[Serializable]
	public class LastTaskId : IExtensible
	{
		private int _taskType;

		private int _taskId;

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

		[ProtoMember(2, IsRequired = false, Name = "taskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
