using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(901), ForSend(901), ProtoContract(Name = "BountyTaskRefreshRes")]
	[Serializable]
	public class BountyTaskRefreshRes : IExtensible
	{
		public static readonly short OP = 901;

		private int _taskId;

		private int _freeCountDown;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "taskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "freeCountDown", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int freeCountDown
		{
			get
			{
				return this._freeCountDown;
			}
			set
			{
				this._freeCountDown = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
