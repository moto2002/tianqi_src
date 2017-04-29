using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(942), ForSend(942), ProtoContract(Name = "TimeLimitedSalesBuyReq")]
	[Serializable]
	public class TimeLimitedSalesBuyReq : IExtensible
	{
		public static readonly short OP = 942;

		private long _goodsNumber;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "goodsNumber", DataFormat = DataFormat.TwosComplement)]
		public long goodsNumber
		{
			get
			{
				return this._goodsNumber;
			}
			set
			{
				this._goodsNumber = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
