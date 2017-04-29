using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6841), ForSend(6841), ProtoContract(Name = "BountyAcceptTaskReq")]
	[Serializable]
	public class BountyAcceptTaskReq : IExtensible
	{
		public static readonly short OP = 6841;

		private int _taskId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
