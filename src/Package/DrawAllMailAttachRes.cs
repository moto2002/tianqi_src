using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(806), ForSend(806), ProtoContract(Name = "DrawAllMailAttachRes")]
	[Serializable]
	public class DrawAllMailAttachRes : IExtensible
	{
		public static readonly short OP = 806;

		private readonly List<long> _mailIds = new List<long>();

		private readonly List<DetailInfo> _itemIds = new List<DetailInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "mailIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> mailIds
		{
			get
			{
				return this._mailIds;
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
