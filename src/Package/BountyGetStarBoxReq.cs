using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(836), ForSend(836), ProtoContract(Name = "BountyGetStarBoxReq")]
	[Serializable]
	public class BountyGetStarBoxReq : IExtensible
	{
		public static readonly short OP = 836;

		private BountyTaskType.ENUM _taskType;

		private int _boxTypeId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "taskType", DataFormat = DataFormat.TwosComplement)]
		public BountyTaskType.ENUM taskType
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

		[ProtoMember(2, IsRequired = false, Name = "boxTypeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int boxTypeId
		{
			get
			{
				return this._boxTypeId;
			}
			set
			{
				this._boxTypeId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
