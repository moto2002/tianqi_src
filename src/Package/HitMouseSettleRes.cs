using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4319), ForSend(4319), ProtoContract(Name = "HitMouseSettleRes")]
	[Serializable]
	public class HitMouseSettleRes : IExtensible
	{
		public static readonly short OP = 4319;

		private int _hadTimes;

		private readonly List<ItemBriefInfo> _dropItem = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "hadTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hadTimes
		{
			get
			{
				return this._hadTimes;
			}
			set
			{
				this._hadTimes = value;
			}
		}

		[ProtoMember(2, Name = "dropItem", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> dropItem
		{
			get
			{
				return this._dropItem;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
