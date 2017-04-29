using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1130), ForSend(1130), ProtoContract(Name = "BountyAccelerateBoxOpenRes")]
	[Serializable]
	public class BountyAccelerateBoxOpenRes : IExtensible
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

		public static readonly short OP = 1130;

		private ulong _uId;

		private readonly List<BountyAccelerateBoxOpenRes.DropItemInfo> _items = new List<BountyAccelerateBoxOpenRes.DropItemInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "uId", DataFormat = DataFormat.TwosComplement), DefaultValue(0f)]
		public ulong uId
		{
			get
			{
				return this._uId;
			}
			set
			{
				this._uId = value;
			}
		}

		[ProtoMember(2, Name = "items", DataFormat = DataFormat.Default)]
		public List<BountyAccelerateBoxOpenRes.DropItemInfo> items
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
