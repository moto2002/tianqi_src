using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3720), ForSend(3720), ProtoContract(Name = "GetExtraRewardRes")]
	[Serializable]
	public class GetExtraRewardRes : IExtensible
	{
		public static readonly short OP = 3720;

		private int _taskType;

		private readonly List<ItemBriefInfo> _item = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "taskType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskType
		{
			get
			{
				return this._taskType;
			}
			set
			{
				this._taskType = value;
			}
		}

		[ProtoMember(2, Name = "item", DataFormat = DataFormat.Default)]
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
