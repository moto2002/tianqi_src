using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1678), ForSend(1678), ProtoContract(Name = "ShopInfoLoginPush")]
	[Serializable]
	public class ShopInfoLoginPush : IExtensible
	{
		public static readonly short OP = 1678;

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
