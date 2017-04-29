using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1472), ForSend(1472), ProtoContract(Name = "ShopRefreshPush")]
	[Serializable]
	public class ShopRefreshPush : IExtensible
	{
		public static readonly short OP = 1472;

		private readonly List<RefreshShopInfo> _shopInfos = new List<RefreshShopInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "shopInfos", DataFormat = DataFormat.Default)]
		public List<RefreshShopInfo> shopInfos
		{
			get
			{
				return this._shopInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
