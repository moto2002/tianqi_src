using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1018), ForSend(1018), ProtoContract(Name = "GetActivityItemPrizeReq")]
	[Serializable]
	public class GetActivityItemPrizeReq : IExtensible
	{
		public static readonly short OP = 1018;

		private int _typeId;

		private int _activityItemId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "activityItemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activityItemId
		{
			get
			{
				return this._activityItemId;
			}
			set
			{
				this._activityItemId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
