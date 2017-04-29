using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(697), ForSend(697), ProtoContract(Name = "DrawMailAttachRes")]
	[Serializable]
	public class DrawMailAttachRes : IExtensible
	{
		public static readonly short OP = 697;

		private long _id;

		private readonly List<DetailInfo> _itemIds = new List<DetailInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, Name = "itemIds", DataFormat = DataFormat.Default)]
		public List<DetailInfo> itemIds
		{
			get
			{
				return this._itemIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
