using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(543), ForSend(543), ProtoContract(Name = "ReqRefineBody")]
	[Serializable]
	public class ReqRefineBody : IExtensible
	{
		public static readonly short OP = 543;

		private readonly List<RefineBodyItemInfo> _itemInfo = new List<RefineBodyItemInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "itemInfo", DataFormat = DataFormat.Default)]
		public List<RefineBodyItemInfo> itemInfo
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
