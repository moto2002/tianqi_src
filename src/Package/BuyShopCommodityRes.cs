using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1682), ForSend(1682), ProtoContract(Name = "BuyShopCommodityRes")]
	[Serializable]
	public class BuyShopCommodityRes : IExtensible
	{
		public static readonly short OP = 1682;

		private int _commodityId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "commodityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int commodityId
		{
			get
			{
				return this._commodityId;
			}
			set
			{
				this._commodityId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
