using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(6692), ForSend(6692), ProtoContract(Name = "OpenShopRes")]
	[Serializable]
	public class OpenShopRes : IExtensible
	{
		public static readonly short OP = 6692;

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
