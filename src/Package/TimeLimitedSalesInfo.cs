using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(851), ForSend(851), ProtoContract(Name = "TimeLimitedSalesInfo")]
	[Serializable]
	public class TimeLimitedSalesInfo : IExtensible
	{
		public static readonly short OP = 851;

		private readonly List<BuyGoodsInfo> _buyGoodsInfo = new List<BuyGoodsInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "buyGoodsInfo", DataFormat = DataFormat.Default)]
		public List<BuyGoodsInfo> buyGoodsInfo
		{
			get
			{
				return this._buyGoodsInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
