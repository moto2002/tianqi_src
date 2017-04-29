using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(899), ForSend(899), ProtoContract(Name = "BountyTaskRefreshReq")]
	[Serializable]
	public class BountyTaskRefreshReq : IExtensible
	{
		public static readonly short OP = 899;

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
