using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4368), ForSend(4368), ProtoContract(Name = "ReceivePrizeReq")]
	[Serializable]
	public class ReceivePrizeReq : IExtensible
	{
		public static readonly short OP = 4368;

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
