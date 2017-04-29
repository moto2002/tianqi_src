using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3665), ForSend(3665), ProtoContract(Name = "BuyReq")]
	[Serializable]
	public class BuyReq : IExtensible
	{
		public static readonly short OP = 3665;

		private int _goodsId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "goodsId", DataFormat = DataFormat.TwosComplement)]
		public int goodsId
		{
			get
			{
				return this._goodsId;
			}
			set
			{
				this._goodsId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
