using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3902), ForSend(3902), ProtoContract(Name = "GetBossLabelInfoReq")]
	[Serializable]
	public class GetBossLabelInfoReq : IExtensible
	{
		public static readonly short OP = 3902;

		private int _labelId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "labelId", DataFormat = DataFormat.TwosComplement)]
		public int labelId
		{
			get
			{
				return this._labelId;
			}
			set
			{
				this._labelId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
