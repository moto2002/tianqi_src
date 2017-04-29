using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(4432), ForSend(4432), ProtoContract(Name = "GetLoginWelfareRes")]
	[Serializable]
	public class GetLoginWelfareRes : IExtensible
	{
		public static readonly short OP = 4432;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
