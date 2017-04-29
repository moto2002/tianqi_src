using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1007), ForSend(1007), ProtoContract(Name = "GuildQuestionRewardNty")]
	[Serializable]
	public class GuildQuestionRewardNty : IExtensible
	{
		public static readonly short OP = 1007;

		private readonly List<ItemBriefInfo> _item = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "item", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> item
		{
			get
			{
				return this._item;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
