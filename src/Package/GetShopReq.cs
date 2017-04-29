using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(419), ForSend(419), ProtoContract(Name = "GetShopReq")]
	[Serializable]
	public class GetShopReq : IExtensible
	{
		public static readonly short OP = 419;

		private int _shopId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "shopId", DataFormat = DataFormat.TwosComplement)]
		public int shopId
		{
			get
			{
				return this._shopId;
			}
			set
			{
				this._shopId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
