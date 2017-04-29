using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(966), ForSend(966), ProtoContract(Name = "GetOpenServerActInfoReq")]
	[Serializable]
	public class GetOpenServerActInfoReq : IExtensible
	{
		public static readonly short OP = 966;

		private OpenServerType.acType _activityType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "activityType", DataFormat = DataFormat.TwosComplement)]
		public OpenServerType.acType activityType
		{
			get
			{
				return this._activityType;
			}
			set
			{
				this._activityType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
