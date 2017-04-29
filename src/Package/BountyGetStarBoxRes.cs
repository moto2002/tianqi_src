using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(838), ForSend(838), ProtoContract(Name = "BountyGetStarBoxRes")]
	[Serializable]
	public class BountyGetStarBoxRes : IExtensible
	{
		[ProtoContract(Name = "DropItemInfo")]
		[Serializable]
		public class DropItemInfo : IExtensible
		{
			private int _cfgId;

			private int _count;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "cfgId", DataFormat = DataFormat.TwosComplement)]
			public int cfgId
			{
				get
				{
					return this._cfgId;
				}
				set
				{
					this._cfgId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "count", DataFormat = DataFormat.TwosComplement)]
			public int count
			{
				get
				{
					return this._count;
				}
				set
				{
					this._count = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 838;

		private readonly List<BountyGetStarBoxRes.DropItemInfo> _items = new List<BountyGetStarBoxRes.DropItemInfo>();

		private BountyTaskType.ENUM _taskType;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<BountyGetStarBoxRes.DropItemInfo> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "taskType", DataFormat = DataFormat.TwosComplement)]
		public BountyTaskType.ENUM taskType
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
