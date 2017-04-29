using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(5939), ForSend(5939), ProtoContract(Name = "SmeltEquipRes")]
	[Serializable]
	public class SmeltEquipRes : IExtensible
	{
		public static readonly short OP = 5939;

		private readonly List<long> _equipIds = new List<long>();

		private readonly List<ItemBriefInfo> _itemInfo = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "equipIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> equipIds
		{
			get
			{
				return this._equipIds;
			}
		}

		[ProtoMember(2, Name = "itemInfo", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> itemInfo
		{
			get
			{
				return this._itemInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
