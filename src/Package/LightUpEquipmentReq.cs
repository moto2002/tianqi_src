using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(848), ForSend(848), ProtoContract(Name = "LightUpEquipmentReq")]
	[Serializable]
	public class LightUpEquipmentReq : IExtensible
	{
		public static readonly short OP = 848;

		private long _equipId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "equipId", DataFormat = DataFormat.TwosComplement)]
		public long equipId
		{
			get
			{
				return this._equipId;
			}
			set
			{
				this._equipId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
