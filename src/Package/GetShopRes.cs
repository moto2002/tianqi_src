using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1554), ForSend(1554), ProtoContract(Name = "GetShopRes")]
	[Serializable]
	public class GetShopRes : IExtensible
	{
		public static readonly short OP = 1554;

		private readonly List<GetShopInfos> _shopInfos = new List<GetShopInfos>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "shopInfos", DataFormat = DataFormat.Default)]
		public List<GetShopInfos> shopInfos
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
