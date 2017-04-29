using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2222), ForSend(2222), ProtoContract(Name = "BuyPacksRes")]
	[Serializable]
	public class BuyPacksRes : IExtensible
	{
		public static readonly short OP = 2222;

		private readonly List<ItemBriefInfo> _itemsInfo = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "itemsInfo", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> itemsInfo
		{
			get
			{
				return this._itemsInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
